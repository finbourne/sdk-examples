using System;
using Finbourne.Horizon.Sdk.Api;
using Finbourne.Horizon.Sdk.Extensions;

namespace Sdk.Examples.Horizon.Utilities
{
    public class TutorialBase
    {
        internal readonly IApiFactory ApiFactory;
        internal readonly IInstrumentApi InstrumentApi;
        internal readonly IProcessHistoryApi ProcessHistoryApi;
        internal readonly IVendorApi VendorApi;

        protected TutorialBase()
        {
            // Initialize all the API end points
            ApiFactory = TestHorizonApiFactoryBuilder.CreateApiFactory("secrets.json");
            InstrumentApi = ApiFactory.Api<IInstrumentApi>();
            ProcessHistoryApi = ApiFactory.Api<IProcessHistoryApi>();
            VendorApi = ApiFactory.Api<IVendorApi>();
        }
    }
}
