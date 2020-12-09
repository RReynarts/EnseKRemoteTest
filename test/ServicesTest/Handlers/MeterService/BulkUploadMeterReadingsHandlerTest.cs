using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Common.Repository;
using Contracts.Requests;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Services.Handlers.MeterService;
using Services.Validators.MeterService;
using Xunit;

namespace ServicesTest.Handlers.MeterService
{
    public class BulkUploadMeterReadingsHandlerTest
    {
        [Fact]
        public void When_Ctor_Context_Is_Null()
        {
            //Arrange
            //Act
            //Assert
            var error = Assert.Throws<ArgumentNullException>(() => new BulkUploadMeterReadingsHandler(null, A.Fake<IDataSetFactory>(), A.Fake<BuklUploadMeterReadingsValidator>()));
            error.Message.Should().Contain("context");
        }

        [Fact]
        public void When_Ctor_Repository_Is_Null()
        {
            //Arrange
            //Act
            //Assert
            var error = Assert.Throws<ArgumentNullException>(() => new BulkUploadMeterReadingsHandler(A.Fake<IUnitOfWork>(), null, A.Fake<BuklUploadMeterReadingsValidator>()));
            error.Message.Should().Contain("repository");
        }

        [Fact]
        public void When_Ctor_Validator_Is_Null()
        {
            //Arrange
            //Act
            //Assert
            var error = Assert.Throws<ArgumentNullException>(() => new BulkUploadMeterReadingsHandler(A.Fake<IUnitOfWork>(), A.Fake<IDataSetFactory>(), null));
            error.Message.Should().Contain("validator");
        }

        [Fact]
        public async Task When_Execute_Uploads_MeterReadings()
        {
            //Arrange
            var repository = A.Fake<IDataSetFactory>();
            var sut = new BulkUploadMeterReadingsHandler(A.Fake<IUnitOfWork>(), repository,
                new BuklUploadMeterReadingsValidator());
            var dataset = A.Fake<IDataSet<Account>>();
            dataset.Add(new Account {AccountId = 2344});
            A.CallTo(() => repository.Resolve<Account>()).Returns(dataset);
            A.CallTo(() => dataset.AnyAsync(A<Expression<Func<Account, bool>>>.Ignored, A<CancellationToken>._)).Returns(true);
            var ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);
            await tw.WriteLineAsync("AccountId,MeterReadingDateTime,MeterReadValue");
            await tw.WriteLineAsync("2344,22/04/2019 09:24,01002");
            await tw.FlushAsync();
            ms.Position = 0;

            var request = new BulkUploadMeterReadingsRequest {UploadFile = ms};

            //Act
            var result = await sut.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.SuccesfulUploads.Should().Be(1);
            result.FailedUploads.Should().Be(0);
            result.UploadErrors.Count.Should().Be(0);
        }

        [Fact]
        public async Task When_Execute_Uploads_MeterReadings_Invalid_Values()
        {
            //Arrange
            var repository = A.Fake<IDataSetFactory>();
            var sut = new BulkUploadMeterReadingsHandler(A.Fake<IUnitOfWork>(), repository,
                new BuklUploadMeterReadingsValidator());
            var accountDataset = A.Fake<IDataSet<Account>>();
            A.CallTo(() => repository.Resolve<Account>()).Returns(accountDataset);
            A.CallTo(() => accountDataset.AnyAsync(A<Expression<Func<Account, bool>>>.Ignored, A<CancellationToken>._)).Returns(false);

            var meterReadingDataset = A.Fake<IDataSet<MeterReading>>();
            A.CallTo(() => repository.Resolve<MeterReading>()).Returns(meterReadingDataset);
            A.CallTo(() => meterReadingDataset.AnyAsync(A<Expression<Func<MeterReading, bool>>>.Ignored, A<CancellationToken>._)).Returns(true);

            var ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);
            await tw.WriteLineAsync("AccountId,MeterReadingDateTime,MeterReadValue");
            await tw.WriteLineAsync("INVALID,22/04/2019 09:24,01002");
            await tw.WriteLineAsync("2344,22/22/2019 09:24,01002");
            await tw.WriteLineAsync("2344,22/04/2019 09:24,100200");
            await tw.WriteLineAsync("2345,22/04/2019 09:24,01002");
            await tw.FlushAsync();
            ms.Position = 0;

            var request = new BulkUploadMeterReadingsRequest {UploadFile = ms};

            //Act
            var result = await sut.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.SuccesfulUploads.Should().Be(0);
            result.FailedUploads.Should().Be(4);
            result.UploadErrors.Count.Should().Be(4);
            result.UploadErrors.Any(e => e.Contains("INVALID' is not a valid number for AccountId")).Should().BeTrue();
            result.UploadErrors.Any(e => e.Contains("22/22/2019 09:24' is not a valid date time for MeterReadingDateTime")).Should().BeTrue();
            result.UploadErrors.Any(e => e.Contains("100200' is not a valid number for MeterReadValue")).Should().BeTrue();
            result.UploadErrors.Any(e => e.Contains("Account with AccountId '2345' not found!")).Should().BeTrue();
        }

        [Fact]
        public async Task When_Execute_Uploads_MeterReadings_Double_Entry()
        {
            //Arrange
            var repository = A.Fake<IDataSetFactory>();
            var sut = new BulkUploadMeterReadingsHandler(A.Fake<IUnitOfWork>(), repository,
                new BuklUploadMeterReadingsValidator());
            var accountDataset = A.Fake<IDataSet<Account>>();
            A.CallTo(() => repository.Resolve<Account>()).Returns(accountDataset);
            A.CallTo(() => accountDataset.AnyAsync(A<Expression<Func<Account, bool>>>.Ignored, A<CancellationToken>._)).Returns(true);

            var meterReadingDataset = A.Fake<IDataSet<MeterReading>>();
            A.CallTo(() => repository.Resolve<MeterReading>()).Returns(meterReadingDataset);
            A.CallTo(() => meterReadingDataset.AnyAsync(A<Expression<Func<MeterReading, bool>>>.Ignored, A<CancellationToken>._)).Returns(true);

            var ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);
            await tw.WriteLineAsync("AccountId,MeterReadingDateTime,MeterReadValue");
            await tw.WriteLineAsync("2345,22/04/2019 09:24,01002");
            await tw.FlushAsync();
            ms.Position = 0;

            var request = new BulkUploadMeterReadingsRequest {UploadFile = ms};

            //Act
            var result = await sut.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.SuccesfulUploads.Should().Be(0);
            result.FailedUploads.Should().Be(1);
            result.UploadErrors.Count.Should().Be(1);
            result.UploadErrors.Any(e => e.Contains("Double entry for values '2345, 22/04/2019 09:24, 01002")).Should().BeTrue();
        }
    }
}
