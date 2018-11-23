using AzureFunction.Data.Model.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace AzureFunction.Data.Model.Extensions
{
    /// <summary>
    /// Result Extension Methods
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Indicates whether the result has errors
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>
        ///   <c>true</c> if the specified results has errors; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasErrors(this IEnumerable<Result> results)
        {
            return results.Contains(m => m.Severity == ResultSeverity.Error);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has <see cref="Result"/>.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>
        ///   <c>true</c> if the specified results has messages; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasMessages(this IEnumerable<Result> results)
        {
            return results.Any();
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains <see cref="Result"/> with a severity of Warning.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>
        ///   <c>true</c> if the specified results has warnings; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasWarnings(this IEnumerable<Result> results)
        {
            return results.Contains(m => m.Severity == ResultSeverity.Warning);
        }
    }
}
