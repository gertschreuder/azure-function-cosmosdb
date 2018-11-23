using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using AzureFunction.Data;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Progress;
using AzureFunction.Data.Model.State;
using AzureFunction.WS.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity;

namespace AzureFunction.WS
{
    public static class Api
    {
        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
             var container = InitialiseUnityContainer();
             return container;
        });

        [FunctionName("QuestEngine_Progress")]
        public static async Task<HttpResponseMessage> Progress([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "progress")]HttpRequestMessage req, ILogger log)
        {
            var json = req.Content.ReadAsStringAsync().Result;
            var request = JsonConvert.DeserializeObject<ProgressRequest>(json);

            return new UpdateProgressAction<HttpResponseMessage>(_container.Value.Resolve<QuestEngineManager>(), log)
            {
                OnFailed = (ex) => req.CreateResponse(HttpStatusCode.BadRequest, ex),
                OnSuccess = (response) => req.CreateResponse(HttpStatusCode.OK, response)
            }.Execute(request);
        }

        [FunctionName("QuestEngine_GetState")]
        public static HttpResponseMessage GetState([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "state")]HttpRequestMessage req, ILogger log)
        {
            var playerId = req.GetQueryNameValuePairs().FirstOrDefault(q => String.Compare(q.Key, "PlayerId", StringComparison.OrdinalIgnoreCase) == 0).Value;

            if (!Guid.TryParse(playerId, out var guidOutput)) req.CreateResponse(HttpStatusCode.BadRequest,"Invalid PlayerId");

             var request = new StateRequest{PlayerId = guidOutput };

            return new GetStateAction<HttpResponseMessage>(_container.Value.Resolve<QuestEngineManager>(), log)
            {
                OnFailed = (ex) => req.CreateResponse(HttpStatusCode.BadRequest, ex),
                OnInvalid = (msg) => req.CreateResponse(HttpStatusCode.NotFound, msg),
                OnSuccess = (response) => req.CreateResponse(HttpStatusCode.OK, response)
            }.Execute(request);
        }

        private static IUnityContainer InitialiseUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IQuestEngineManager, QuestEngineManager>();
            container.RegisterType<IRepository, DocumentRepository>();
            container.RegisterType<ILogger, Logger<object>>();
            return container;
        }
    }
}
