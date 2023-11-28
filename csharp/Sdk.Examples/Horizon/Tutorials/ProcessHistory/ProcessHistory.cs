using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Finbourne.Horizon.Sdk.Api;
using Finbourne.Horizon.Sdk.Client;
using Finbourne.Horizon.Sdk.Model;
using NUnit.Framework;
using Sdk.Examples.Horizon.Utilities;

namespace Sdk.Examples.Horizon.Tutorials.ProcessHistory
{
    [TestFixture]
    public class ProcessHistory : TutorialBase
    {
        [OneTimeSetUp]
        public void SetUp()
        {

        }

        [OneTimeTearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Get_Latest_Runs()
        {
            var processInformation = ProcessHistoryApi.GetLatestRuns();
            // Console.WriteLine(processInformation.ToString());
        }

        [Test]
        public void Process_History_Entries()
        {
            var body = "{}";
            var processInformation = ProcessHistoryApi.ProcessHistoryEntries(body);
            // Console.WriteLine(processInformation.ToString());
        }

        [Test]
        public void Process_Entry_Updates()
        {
            var body = "{}";
            var processUpdateResult = ProcessHistoryApi.ProcessEntryUpdates(body);
            // Console.WriteLine(processUpdateResult.ToString());
        }

        [Test]
        public void Create_Complete_Event()
        {
            var auditCompleteRequest = new AuditCompleteRequest(
                "string",
                "string",
                new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                "string",
                AuditCompleteStatus.Succeeded,
                0,
                0,
                0,
                0,
                new List<AuditFileDetails>{new AuditFileDetails(AuditFileType.SourceData, "string")}
            );
            var processUpdateResult = ProcessHistoryApi.CreateCompleteEvent(auditCompleteRequest);
            // Console.WriteLine(processUpdateResult.ToString());
        }

        [Test]
        public void Create_Update_Event()
        {
            var auditUpdateRequest = new AuditUpdateRequest(
                "string",
                "string",
                new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                "string"
            );
            var processUpdateResult = ProcessHistoryApi.CreateUpdateEvent(auditUpdateRequest);
            // Console.WriteLine(processUpdateResult.ToString());
        }
    }
}