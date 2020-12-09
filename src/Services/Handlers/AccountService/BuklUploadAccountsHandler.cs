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
using Services.Validators.AccountService;

namespace Services.Handlers.AccountService
{
    public class
        BuklUploadAccountsHandler : CommandRequestHandler<BulkUploadAccountsRequest,
            BulkUploadAccountsResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly BuklUploadAccountsValidator _validator;

        public BuklUploadAccountsHandler(IUnitOfWork context, IDataSetFactory repository,
            BuklUploadAccountsValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<BulkUploadAccountsResponse> Execute(
            BulkUploadAccountsRequest request)
        {
            _validator.Validate(request);
            request.UploadFile.Position = 0;

            var accountLines = ReadLines(request.UploadFile).Skip(1).Select(FromCsv).ToList();

            var bulkUploadAccountsResponse = new BulkUploadAccountsResponse
            {
                UploadErrors = new List<string>()
            };

            foreach (var accountLine in accountLines)
            {
                var validAccountId = int.TryParse(accountLine.AccountId, out var accountId);
                if (!validAccountId)
                {
                    bulkUploadAccountsResponse.UploadErrors.Add(string.Format(ValidationMessages.NotANumber,
                        accountLine.AccountId, nameof(accountLine.AccountId)));
                    bulkUploadAccountsResponse.FailedUploads++;
                    continue;
                }

                var validFirstName = !string.IsNullOrEmpty(accountLine.FirstName);
                if (!validFirstName)
                {
                    bulkUploadAccountsResponse.UploadErrors.Add(string.Format(ValidationMessages.ValidValueCustom,
                        accountLine.FirstName, nameof(accountLine.FirstName)));
                    bulkUploadAccountsResponse.FailedUploads++;
                    continue;
                }

                var validLastName = !string.IsNullOrEmpty(accountLine.LastName);
                if (!validLastName)
                {
                    bulkUploadAccountsResponse.UploadErrors.Add(string.Format(ValidationMessages.ValidValueCustom,
                        accountLine.LastName, nameof(accountLine.LastName)));
                    bulkUploadAccountsResponse.FailedUploads++;
                    continue;
                }
                
                var doubleEntry = await _repository.Resolve<Account>().AnyAsync(r =>
                    r.AccountId == accountId);

                if (doubleEntry)
                {
                    bulkUploadAccountsResponse.UploadErrors.Add(string.Format(ValidationMessages.DoubleEntry,
                        accountId, accountLine.FirstName, accountLine.LastName));
                    bulkUploadAccountsResponse.FailedUploads++;
                    continue;
                }

                _repository.Resolve<Account>().Add(new Account
                {
                    AccountId = accountId,
                    FirstName = accountLine.FirstName,
                    LastName = accountLine.LastName
                });

                bulkUploadAccountsResponse.SuccesfulUploads++;
            }

            return bulkUploadAccountsResponse;
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

        private static AccountLine FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');
            var accountLine = new AccountLine
            {
                AccountId = values[0],
                FirstName = values[1],
                LastName = values[2]
            };
            return accountLine;
        }
    }
}