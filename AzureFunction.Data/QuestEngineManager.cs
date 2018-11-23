using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Notifications;
using AzureFunction.Data.Model.Progress;
using AzureFunction.Data.Model.State;
using System;
using Microsoft.Extensions.Logging;
using AzureFunction.Data.Model.Extensions;

namespace AzureFunction.Data
{
    public class QuestEngineManager : IQuestEngineManager
    {
        private readonly IRepository repository;
        private ResultCollection result;
        private const string DOES_NOT_EXIST_MESSAGE = "Entity does not exist.";

        public QuestEngineManager(IRepository repository)
        {
            this.repository = repository;
            result = ResultCollection.CreateEmpty();
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ResultCollection GetState(StateRequest request)
        {
            try
            {
                result = repository.GetAsync<ProgressEntity>(request.PlayerId).Result;

                var response = new StateResponse();
                var doc = result.Payload as ProgressEntity;

                if (doc.IsNull()) throw new Exception(DOES_NOT_EXIST_MESSAGE);

                response.MapResponse(doc);

                result.Payload = response;
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            return result;
        }

        /// <summary>
        /// Updates the progress.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// </exception>
        public ResultCollection UpdateProgress(ProgressRequest request)
        {
            try
            {
                result = repository.GetAsync<ProgressEntity>(request.PlayerId).Result;

                var entity = result.Payload as ProgressEntity;
                if (entity.IsNull()) throw new Exception(DOES_NOT_EXIST_MESSAGE);

                entity.PlayerId = request.PlayerId.ToString();
                request.CalculateQuestProgress(entity);

                result = repository.UpdateAsync<ProgressEntity>(entity, entity.PlayerId).Result;

                var response = new ProgressResponse();
                var doc = result.Payload as ProgressEntity;

                if (doc.IsNull()) throw new Exception(DOES_NOT_EXIST_MESSAGE);

                response.MapResponse(doc);

                result.Payload = response;
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            return result;
        }
    }
}
