using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureFunction.Data.Model.Extensions
{
    /// <summary>
    /// Additional extensions on System.Collections.Generic.IEnumerable`1
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Determines whether a sequence contains a specified element(s) by using a predicate
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified predicate]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Any();
        }
    }
}