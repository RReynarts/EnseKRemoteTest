using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;
using Services.Models;
using Services.Validators;
using Services.Validators.MeterService;

namespace Services.Handlers.MeterService
{
    public class
        BulkUploadMeterReadingsHandler : CommandRequestHandler<BulkUploadMeterReadingsRequest,
            BulkUploadMeterReadingsResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly BuklUploadMeterReadingsValidator _validator;

        public BulkUploadMeterReadingsHandler(IUnitOfWork context, IDataSetFactory repository,
            BuklUploadMeterReadingsValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<BulkUploadMeterReadingsResponse> Execute(
            BulkUploadMeterReadingsRequest request)
        {
            _validator.Validate(request);
            request.UploadFile.Position = 0;

            var meterReadingLines = ReadLines(request.UploadFile).Skip(1).Select(FromCsv).ToList();

            var bulkUploadMeterReadingsResponse = new BulkUploadMeterReadingsResponse
            {
                UploadErrors = new List<string>()
            };

            foreach (var meterReadingLine in meterReadingLines)
            {
                var validAccountId = int.TryParse(meterReadingLine.AccountId, out var accountId);
                if (!validAccountId)
                {
                    bulkUploadMeterReadingsResponse.UploadErrors.Add(string.Format(ValidationMessages.NotANumber,
                        meterReadingLine.AccountId, nameof(meterReadingLine.AccountId)));
                    bulkUploadMeterReadingsResponse.FailedUploads++;
                    continue;
                }

                var validMeterReadingDateTime =
                    DateTime.TryParse(meterReadingLine.MeterReadingDateTime, out var meterReadingDateTime);
                if (!validMeterReadingDateTime)
                {
                    bulkUploadMeterReadingsResponse.UploadErrors.Add(string.Format(ValidationMessages.InvalidDateTime,
                        meterReadingLine.MeterReadingDateTime, nameof(meterReadingLine.MeterReadingDateTime)));
                    bulkUploadMeterReadingsResponse.FailedUploads++;
                    continue;
                }

                var validMeterReadValue = int.TryParse(meterReadingLine.MeterReadValue, out var meterReadValue) &&
                                          meterReadValue <= 99999;
                if (!validMeterReadValue)
                {
                    bulkUploadMeterReadingsResponse.UploadErrors.Add(string.Format(ValidationMessages.NotANumber,
                        meterReadingLine.MeterReadValue, nameof(meterReadingLine.MeterReadValue)));
                    bulkUploadMeterReadingsResponse.FailedUploads++;
                    continue;
                }

                var accountExists = await _repository.Resolve<Account>()
                    .AnyAsync(a => a.AccountId == accountId);
                if (!accountExists)
                {
                    bulkUploadMeterReadingsResponse.UploadErrors.Add(string.Format(ValidationMessages.AccountNotFound,
                        meterReadingLine.AccountId));
                    bulkUploadMeterReadingsResponse.FailedUploads++;
                    continue;
                }

                var doubleEntry = await _repository.Resolve<MeterReading>().AnyAsync(r =>
                    r.AccountId == accountId && r.MeterReadValue == meterReadValue &&
                    r.MeterReadingDateTime == meterReadingDateTime);

                if (doubleEntry)
                {
                    bulkUploadMeterReadingsResponse.UploadErrors.Add(string.Format(ValidationMessages.DoubleEntry,
                        accountId, meterReadingDateTime.ToString("dd/MM/yyyy HH:mm"), meterReadValue.ToString("D5")));
                    bulkUploadMeterReadingsResponse.FailedUploads++;
                    continue;
                }

                _repository.Resolve<MeterReading>().Add(new MeterReading
                {
                    AccountId = accountId,
                    MeterReadValue = meterReadValue,
                    MeterReadingDateTime = meterReadingDateTime
                });

                bulkUploadMeterReadingsResponse.SuccesfulUploads++;
            }

            return bulkUploadMeterReadingsResponse;
        }

        private static IEnumerable<string> ReadLines(Stream stream)
        {
            var lines = new List<string>();

            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            return lines;
        }

        private static MeterReadingLine FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');
            var meterReadingLine = new MeterReadingLine
            {
                AccountId = values[0],
                MeterReadingDateTime = values[1],
                MeterReadValue = values[2]
            };
            return meterReadingLine;
        }
    }
}