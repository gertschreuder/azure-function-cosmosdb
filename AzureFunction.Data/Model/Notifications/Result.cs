using System.Globalization;
using System.Runtime.Serialization;

namespace AzureFunction.Data.Model.Notifications
{
    public class Result
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="severity">The severity.</param>
        public Result(string text, ResultSeverity severity)
        {
            Guard.ArgumentNotEmpty(text, "text");
            Text = text;
            Severity = severity;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The <see cref="ResultSeverity"/> of the message
        /// </summary>
        public ResultSeverity Severity { get; set; }

        /// <summary>
        /// The <see cref="Result"/> text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The <see cref="Result"/> code.
        /// </summary>
        public string Code { get; set; }

        #endregion Properties

        #region Virtual Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} : {1}", Severity, Text);
        }

        #endregion Virtual Methods
    }
}
