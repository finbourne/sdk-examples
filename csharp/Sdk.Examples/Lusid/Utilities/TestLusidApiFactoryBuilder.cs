using System.IO;
using Lusid.Sdk.Extensions;

namespace Sdk.Examples.Lusid.Utilities
{
    public class TestLusidApiFactoryBuilder
    {
        public static IApiFactory CreateApiFactory(string secretsFile)
        {
            return File.Exists(secretsFile)
                ? ApiFactoryBuilder.Build(secretsFile)
                : ApiFactoryBuilder.Build(null);
        }
        
        public static ApiConfiguration CreateApiConfiguration(string secretsFile)
        {
            return File.Exists(secretsFile)
                ? ApiConfigurationBuilder.Build(secretsFile)
                : ApiConfigurationBuilder.Build(null);
        }
    }
}