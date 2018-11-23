using System;
using System.Diagnostics;
using AzureFunction.Data.Model.Extensions;

namespace AzureFunction.Data.Model
{
    /// <summary>
    /// Guard clause is a watered down verification mechanism to incorporate a design by contract approach.
    /// Design By Contract (DbC) is a software correctness methodology. It uses preconditions and post conditions
    /// to document (or programmatically assert) the change in state caused by a piece of a program.
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Validates that the string <paramref name="argument"/> is not empty.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException">The provided string argument {0} cannot be empty".FormatCurrentCulture(argumentName)</exception>
        public static void ArgumentNotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentOutOfRangeException("The provided string argument {0} cannot be empty".FormatCurrentCulture(argumentName));
            }
        }

        /// <summary>
        /// Validates that the <paramref name="argument"/> is not null.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            ArgumentNotEmpty(argumentName, "argumentName");

            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates that the <paramref name="value"/> is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="instanceName">Name of the instance.</param>
        /// <exception cref="NullReferenceException">Instance expected but not supplied. " + instanceName</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public static void InstanceNotNull(object value, string instanceName)
        {
            if (value == null)
            {
                throw new NullReferenceException("Instance expected but not supplied. " + instanceName);
            }
        }
    }
}
