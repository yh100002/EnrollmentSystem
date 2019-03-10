using EnrollmentLogic.Configuration;
using Microsoft.Extensions.Options;
using Nest;

namespace EnrollmentLogic.Helpers
{
      public class ElasticClientProvider
    {
        public ElasticClientProvider(IOptions<ElasticConnectionSettings> settings)
        {        
            // Create the connection settings
            ConnectionSettings connectionSettings = new ConnectionSettings(new System.Uri(settings.Value.ClusterUrl));
            // This is going to enable us to see the raw queries sent to elastic when debugging (really useful)
            connectionSettings.EnableDebugMode();
           
           
            this.Client = new ElasticClient(connectionSettings);       
        }
        public ElasticClient Client { get; }
    }
}