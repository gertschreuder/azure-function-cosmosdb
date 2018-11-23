using Microsoft.Extensions.Logging;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Progress;
using System;
using AzureFunction.WS.Models;

namespace AzureFunction.WS.Actions
{
    public class UpdateProgressAction<TResult> : BaseAction<TResult> where TResult : class
    {
        public UpdateProgressAction(IQuestEngineManager manager, ILogger logger) : base(manager, logger)
        {
        }

        public Func<ProgressResponse, TResult> OnSuccess { get; set; }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// </exception>
        public TResult Execute(ProgressRequest request)
        {
            try
            {
                if (!request.IsValid()) throw new Exception(Constants.PROGRESS_VALIDATION_ERROR);

                var response = manager.UpdateProgress(request);

                if(response.HasErrors()) throw new Exception(response.ToString());

                return OnSuccess(response.Payload as ProgressResponse);
            }
            catch (Exception ex)
            {
                Log((int)Models.EventId.SubmissionFailed, ex);
                return OnFailed(ex);
            }
        }
    }
}