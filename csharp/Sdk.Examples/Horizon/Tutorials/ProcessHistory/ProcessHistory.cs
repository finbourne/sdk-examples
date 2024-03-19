using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            // Declare args shared between API calls
            const string id = "CreateThenCompleteEvent";
            const string userId = "ProcessHistoryTutorial";
            var guid = new Guid("91d94df8-f717-41f8-a2a5-e498e41096e5");
            var startTime = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            const string message = "Create event";
            
            // Create new event
            var auditUpdateRequest = new AuditUpdateRequest(
                id,
                userId,
                guid.ToString(),
                startTime,
                message
            );
            var createEventResult = await ProcessHistoryApi.CreateUpdateEventAsync(auditUpdateRequest);

            // Complete existing event
            var auditCompleteRequest = new AuditCompleteRequest(
                id,
                userId,
                guid.ToString(),
                startTime,
                new DateTimeOffset(2018, 1, 2, 0, 0, 0, TimeSpan.Zero),
                message,
                AuditCompleteStatus.Succeeded,
                0,
                0,
                0,
                0,
                new List<AuditFileDetails> { new(AuditFileType.SourceData, "path/to/file/fileName") }
            );
            var completeEventResult = await ProcessHistoryApi.CreateCompleteEventAsync(auditCompleteRequest);

            // Check calls succeeded
            Assert.That(createEventResult.ProcessName.Contains("CreateThenCompleteEvent"));
            Assert.That(completeEventResult.ProcessName.Contains("CreateThenCompleteEvent"));
        }
        
        [Test, Order(2)]
        [Explicit]
        public async Task Get_Latest_Runs()
        {
            var processInformation = await ProcessHistoryApi.GetLatestRunsAsync();
            Assert.That(processInformation, Has.Count.GreaterThanOrEqualTo(2));
        }
    }
}