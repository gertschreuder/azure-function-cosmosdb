using Microsoft.Extensions.Logging;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.State;
using System;
using AzureFunction.WS.Models;

namespace AzureFunction.WS.Actions
{
    public class GetStateAction<TResult> : BaseAction<TResult> where TResult : class
    {
        public GetStateAction(IQuestEngineManager manager, ILogger logger) : base(manager, logger)
        {
        }

        public Func<StateResponse, TResult> OnSuccess { get; set; }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public TResult Execute(StateRequest request)
        {
            try
            {
                if (request.PlayerId == Guid.Empty)
                {
                    return OnInvalid(Constants.ON_INVALID_ERROR);
                }

                var response = manager.GetState(request);

                if (response.HasErrors()) throw new Exception(response.ToString());

                return OnSuccess(response.Payload as StateResponse);
            }
            catch (Exception ex)
            {
                Log((int)Models.EventId.QueryFailed, ex);
                return OnFailed(ex);
            }
        }
    }
}