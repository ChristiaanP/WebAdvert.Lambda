using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using System;
using Nest;
using Newtonsoft.Json;
using AdvertApi.Models.Messages;
using System.Threading.Tasks;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    public class SearchWorker
    {
        public SearchWorker():this(ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance))
        {

        }

        private readonly  IElasticClient _client;
        public SearchWorker(IElasticClient client)
        {
            _client = client;
        }

        public async Task Function(SNSEvent snsEvent, ILambdaContext context)
        {

            
            foreach(var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);
                var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDoc = MappingHelper.MapAdvert(message);
                await _client.IndexDocumentAsync(advertDoc);
            }
        }

    }
}
