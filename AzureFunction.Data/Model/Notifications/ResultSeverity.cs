namespace AzureFunction.Data.Model.Notifications
{
    /// <summary>
    /// Gives an indication of the status or severity of a <see cref="Result"/>
    /// </summary>
    public enum ResultSeverity
    {
        /// <summary>
        /// <see cref="Result"/> contains informational detail.
        /// </summary>
        Information,

        /// <summary>
        /// <see cref="Result"/> contains a warning.
        /// </summary>
        Warning,

        /// <summary>
        /// <see cref="Result"/> contains an error.
        /// </summary>
        Error
    }
}
