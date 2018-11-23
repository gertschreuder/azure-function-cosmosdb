using Microsoft.Extensions.Logging;
using AzureFunction.Data.Contracts;
using System;
using System.Diagnostics;

namespace AzureFunction.WS.Actions
{
    public class BaseAction<T> where T : class
    {
        /// <summary>
        /// Failed func
        /// </summary>
        public Func<Exception, T> OnFailed { get; set; }
        /// <summary>
        /// Failed func
        /// </summary>
        public Func<string, T> OnInvalid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IQuestEngineManager manager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private ILogger logger { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public BaseAction(IQuestEngineManager manager, ILogger logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        protected void Log(int id, Exception ex)
        {
            if (logger != null)
            {
                var eventId = new EventId((int)Models.EventId.SubmissionFailed);
                logger.LogError(eventId, ex, ex.Message);
            }
            else
            {
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}