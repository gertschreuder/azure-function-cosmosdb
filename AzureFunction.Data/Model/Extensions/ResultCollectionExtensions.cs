using AzureFunction.Data.Model.Notifications;
using System;

namespace AzureFunction.Data.Model.Extensions
{
    /// <summary>
    /// Extends a NotificationCollection instance with convenience methods
    /// </summary>
    public static class ResultCollectionExtensions
    {
        /// <summary>
        /// Provides the ability to add a  <see cref="System.Exception"/> as a Notification message
        /// </summary>
        public static ResultCollection AddException(this ResultCollection instance, Exception exception)
        {
            return instance.AddError(exception.Message);
        }


        /// <summary>
        /// Provides the ability to add an error as a Notification message.
        /// </summary>
        public static ResultCollection AddError(this ResultCollection instance, string error)
        {
            return instance.AddError(error, string.Empty);
        }

        /// <summary>
        /// Provides the ability to add an error as a Notification message with an optional error code.
        /// </summary>
        public static ResultCollection AddError(this ResultCollection instance, string error, string errorCode)
        {
            var notification = new Result(error, ResultSeverity.Error);

            if (errorCode.IsNotNullOrEmpty())
            {
                notification.Code = errorCode;
            }

            instance.AddMessage(notification);

            return instance;
        }
    }
}
