using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
        [Test, Order(1)]
        public async Task Create_Update_And_Complete_Event()
        {
            var auditUpdateRequest = new AuditUpdateRequest(
                "CreateThenCompleteEvent",
                "ProcessHistoryTutorial",
                new Guid("91d94df8-f717-41f8-a2a5-e498e41096e5"),
                new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                "Create event"
            );
            var createEventResult = await ProcessHistoryApi.CreateUpdateEventAsync(auditUpdateRequest);

            var auditCompleteRequest = new AuditCompleteRequest(
                "CreateThenCompleteEvent",
                "ProcessHistoryTutorial",
                new Guid("91d94df8-f717-41f8-a2a5-e498e41096e5"),
                new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2018, 1, 2, 0, 0, 0, TimeSpan.Zero),
                "Complete event",
                AuditCompleteStatus.Succeeded,
                0,
                0,
                0,
                0,
                new List<AuditFileDetails> { new(AuditFileType.SourceData, "path/to/file/fileName") }
            );
            var completeEventResult = await ProcessHistoryApi.CreateCompleteEventAsync(auditCompleteRequest);

            Assert.That(createEventResult, Is.Not.Null);
            Assert.That(createEventResult.ProcessName.Contains("CreateThenCompleteEvent"));

            Assert.That(completeEventResult, Is.Not.Null);
            Assert.That(completeEventResult.ProcessName.Contains("CreateThenCompleteEvent"));
        }
        
        [Test, Order(2)]
        public async Task Get_Latest_Runs()
        {
            var processInformation = await ProcessHistoryApi.GetLatestRunsAsync();
            Assert.That(processInformation, Has.Count.GreaterThanOrEqualTo(2));
        }
    }
}