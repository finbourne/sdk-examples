using System;
using System.Collections.Generic;
using System.Linq;
using Finbourne.Horizon.Sdk.Api;
using Finbourne.Horizon.Sdk.Extensions;
using Finbourne.Horizon.Sdk.Model;

namespace Sdk.Examples.Example
{
    public class Example
    {
        public ProcessSummary Run()
        {
            // tag::create-client-factory[]
            var secretsFile = "secrets.json";
            var apiFactory = ApiFactoryBuilder.Build(secretsFile);
            // end::create-client-factory[]

            // tag::create-process-history-api[]
            var processHistoryApi = apiFactory.Api<IProcessHistoryApi>();
            // end::create-process-history-api[]

            // tag::get-latest-runs[]
            var latestRuns = processHistoryApi.GetLatestRuns();
            Console.WriteLine(latestRuns.ToString());
            // end::get-latest-runs[]

            return latestRuns.ProcessSummary;
        }
    }
}