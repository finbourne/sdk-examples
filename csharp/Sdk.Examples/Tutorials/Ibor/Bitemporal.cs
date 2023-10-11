using System;
using System.Collections.Generic;
using System.Threading;
using Lusid.Sdk.Api;
using Lusid.Sdk.Model;
using NUnit.Framework;
using Sdk.Examples.Utilities;

namespace Sdk.Examples.Tutorials.Ibor
{
    [TestFixture]
    public class Bitemporal: TutorialBase
    {

        private IList<string> _instrumentIds;
        [OneTimeSetUp]
        public void SetUp()
        {
            
            var instrumentLoader = new InstrumentLoader(ApiFactory);
            _instrumentIds = instrumentLoader.LoadInstruments();
        }
        
        [Test]
        public void Apply_Bitemporal_Portfolio_Change()
        {
            //var portfolioId = _testDataUtilities.CreateTransactionPortfolio(TestDataUtilities.TutorialScope);
            var portfolioRequest = TestDataUtilities.BuildTransactionPortfolioRequest();
            var portfolio = TransactionPortfoliosApi.CreatePortfolio(TestDataUtilities.TutorialScope, portfolioRequest);
            Assert.That(portfolio?.Id.Code, Is.EqualTo(portfolioRequest.Code));
            var newTransactions = new List<TransactionRequest>
            {
                TestDataUtilities.BuildTransactionRequest(_instrumentIds[0], 100, 101, "GBP", new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero), "Buy"),
                TestDataUtilities.BuildTransactionRequest(_instrumentIds[1], 100, 102, "GBP", new DateTimeOffset(2018, 1, 2, 0, 0, 0, TimeSpan.Zero), "Buy"),
                TestDataUtilities.BuildTransactionRequest(_instrumentIds[2], 100, 103, "GBP", new DateTimeOffset(2018, 1, 3, 0, 0, 0, TimeSpan.Zero), "Buy"),
            };
            
            //    add initial batch of transactions
            var initialResult = ApiFactory.Api<ITransactionPortfoliosApi>().UpsertTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code, newTransactions);

            var asAtBatch1 = initialResult._Version.AsAtDate;
            Thread.Sleep(500);

            //    add another transaction for 2018-1-8
            var laterResult = ApiFactory.Api<ITransactionPortfoliosApi>().UpsertTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code, new List<TransactionRequest>
            {
                TestDataUtilities.BuildTransactionRequest(_instrumentIds[3], 100, 104, "GBP", new DateTimeOffset(2018, 1, 8, 0, 0, 0, TimeSpan.Zero), "Buy"),
            });

            var asAtBatch2 = laterResult._Version.AsAtDate;
            Thread.Sleep(500);

            //    add back-dated transaction
            var backDatedResult = ApiFactory.Api<ITransactionPortfoliosApi>().UpsertTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code, new List<TransactionRequest>
            {
                TestDataUtilities.BuildTransactionRequest(_instrumentIds[4], 100, 105, "GBP", new DateTimeOffset(2018, 1, 5, 0, 0, 0, TimeSpan.Zero), "Buy"),
            });

            var asAtBatch3 = backDatedResult._Version.AsAtDate;
            Thread.Sleep(500);

            //    list transactions
            var transactions = ApiFactory.Api<ITransactionPortfoliosApi>().GetTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code, asAt: asAtBatch1);
            
            Assert.That(transactions.Values.Count, Is.EqualTo(3), $"AsAt: {asAtBatch1:o}");

            transactions = ApiFactory.Api<ITransactionPortfoliosApi>().GetTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code, asAt: asAtBatch2);
            
            Assert.That(transactions.Values.Count, Is.EqualTo(4), $"AsAt: {asAtBatch2:o}");

            transactions = ApiFactory.Api<ITransactionPortfoliosApi>().GetTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code, asAt: asAtBatch3);
            
            Assert.That(transactions.Values.Count, Is.EqualTo(5), $"AsAt: {asAtBatch3:o}");

            transactions = ApiFactory.Api<ITransactionPortfoliosApi>().GetTransactions(TestDataUtilities.TutorialScope, portfolioRequest.Code);
        }
    }
}