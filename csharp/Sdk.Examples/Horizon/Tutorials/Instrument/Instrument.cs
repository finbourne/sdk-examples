using System.Threading.Tasks;
using Finbourne.Horizon.Sdk.Model;
using NUnit.Framework;
using Sdk.Examples.Horizon.Utilities;

namespace Sdk.Examples.Horizon.Tutorials.Instrument
{
    [TestFixture]
    public class Instrument : TutorialBase
    {
        [Test]
        public async Task Search_OpenFigi_Query()
        {
            // Query OpenFigi
            var openFigiQueryResult = await InstrumentApi.SearchOpenFigiAsync("VOD", false);
            Assert.That(openFigiQueryResult.Results, Is.Not.Empty);
        }

        [Test]
        public async Task Search_OpenFigi_Market_Sector()
        {
            // Check Equity is a valid Market Sector 
            var allowedMarketSectors =
                await InstrumentApi.GetOpenFigiParameterOptionAsync(OpenFigiParameterOptionName.MarketSector);
            Assert.That(allowedMarketSectors, Contains.Item(new AllowedParameterValue("Equity")));

            // Query OpenFigi with Market Sector filter
            var openFigiMarketSectorResult =
                await InstrumentApi.SearchOpenFigiAsync("VOD", false, 25, "Equity");
            Assert.That(openFigiMarketSectorResult.Results, Is.Not.Empty);
        }

        [Test]
        public async Task Search_OpenFigi_PermId()
        {
            // Query OpenFigi and enrich with PermId data
            var openFigiPermIdResult =
                await InstrumentApi.SearchOpenFigiAsync("VOD", true);
            Assert.That(openFigiPermIdResult.Results, Is.Not.Empty);
        }
        
        [Test]
        public async Task Vendors(){
            // Check Common Stock is a valid Security Type
            var allowedSecurityType =
                await InstrumentApi.GetOpenFigiParameterOptionAsync(OpenFigiParameterOptionName.SecurityType);
            Assert.That(allowedSecurityType.Contains(new AllowedParameterValue("Common Stock")));

            // Get VendorProducts of any supported and licensed integrations
            // filtered by Market Sector and Security Type
            var vendorsResult = 
                await InstrumentApi.VendorsAsync("Equity", "Common Stock");
            Assert.That(vendorsResult, Has.One.Matches<VendorProduct>(v => v.VendorName == "Bloomberg"));
            Assert.That(vendorsResult, Has.One.Matches<VendorProduct>(v => v.VendorName == "SIX"));
        }
    }
}