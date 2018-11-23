namespace AzureFunction.WS.Models
{
    /// <summary>
    /// Enumeration of all different EventId that can be used for logging
    /// </summary>
    internal enum EventId
    {
        SubmissionSucceeded = 1000,
        SubmissionFailed = 1001,
        QuerySucceeded = 1100,
        QueryFailed = 1101,
    }
}
