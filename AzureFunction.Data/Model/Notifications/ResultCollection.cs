using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AzureFunction.Data.Model.Extensions;

namespace AzureFunction.Data.Model.Notifications
{
    /// <summary>
    /// Collection of <see cref="Result"/>.
    /// </summary>
    public sealed class ResultCollection : IFormattable, IEnumerable<Result>
    {
        private readonly List<Result> messages = new List<Result>();

        #region Methods

        /// <summary>
        /// Gets a value indicating whether this instance has <see cref="Result"/>.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has <see cref="Result"/>; otherwise, <c>false</c>.
        /// </value>
        public bool HasMessages()
        {
            return messages.HasMessages();
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains <see cref="Result"/> with a severity of Warning.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has warnings; otherwise, <c>false</c>.
        /// </value>
        public bool HasWarnings()
        {
            return messages.HasWarnings();
        }

        /// <summary>
        /// Indicates whether the result has errors
        /// </summary>
        /// <returns></returns>
        public bool HasErrors()
        {
            return messages.HasErrors();
        }

        /// <summary>
        /// Adds a <see cref="Result"/> to the <see cref="ResultCollection"/>
        /// </summary>
        /// <param name="result">The message to add</param>
        public void AddMessage(Result result)
        {
            if (result != null)
            {
                messages.Add(result);
            }
        }

        /// <summary>
        /// Adds a <see cref="Result"/> list to the <see cref="ResultCollection"/>
        /// </summary>
        /// <param name="results">The message to add</param>
        public void AddMessage(IEnumerable<Result> results)
        {
            if (results != null)
            {
                messages.AddRange(results);
            }
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing the value of the current instance in the specified format.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var builder = new StringBuilder();
            foreach (var message in messages)
            {
                builder.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1}", builder.Length > 0 ? Environment.NewLine : null, message));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Creates an empty <see cref="ResultCollection"/>.
        /// </summary>
        /// <returns></returns>
        public static ResultCollection CreateEmpty()
        {
            return new ResultCollection();
        }

        /// <summary>
        /// Creates a <see cref="ResultCollection"/> that contains <paramref name="result"/>
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static ResultCollection Create(Result result)
        {
            ResultCollection resultCollection = CreateEmpty();
            resultCollection.AddMessage(result);
            return resultCollection;
        }

        /// <summary>
        /// Creates a <see cref="ResultCollection"/> that contains the entire list from <paramref name="results"/>
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns></returns>
        public static ResultCollection Create(IList<Result> results)
        {
            var resultCollection = CreateEmpty();
            resultCollection.AddMessage(results);
            return resultCollection;
        }

        #endregion

        #region Operator Overloads

        /// <summary>
        /// Adds one <see cref="ResultCollection"/> to another <see cref="ResultCollection"/>
        /// </summary>
        /// <param name="left">The first <see cref="ResultCollection"/></param>
        /// <param name="right">The second <see cref="ResultCollection"/></param>
        /// <returns></returns>
        public static ResultCollection Add(ResultCollection left, ResultCollection right)
        {
            return left + right;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1013:OverloadOperatorEqualsOnOverloadingAddAndSubtract", Justification = "Not interested to verify equality")]
        public static ResultCollection operator +(ResultCollection left, ResultCollection right)
        {
            Guard.ArgumentNotNull(left, "left");

            if (right != null)
            {
                left.AddMessage(right);
            }
            return left;
        }

        /// <summary>
        /// Implements the operator + on a <see cref="ResultCollection"/> and a <see cref="Result"/>.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static ResultCollection operator +(ResultCollection left, Result right)
        {
            Guard.ArgumentNotNull(left, "left");

            if (right != null)
            {
                left.AddMessage(right);
            }
            return left;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ResultCollection"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ResultCollection result)
        {
            return result.ToString();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<Result> IEnumerable<Result>.GetEnumerator()
        {
            for (int i = 0; i < messages.Count; i++)
            {
                yield return messages[i];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Result>)this).GetEnumerator();
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the Payload
        /// </summary>
        public object Payload { get; set; }

        #endregion
    }
}
