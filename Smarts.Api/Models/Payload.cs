using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    public class Payload<T>
    {
        private bool isSuccess = false;

        /// <summary>
        /// The status code of the payload.
        /// </summary>
        public PayloadStatusCode Code { get; set; }

        /// <summary>
        /// The actual data the payload contains.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// A list of errors that have occurred while working with the payload.
        /// </summary>
        public Dictionary<string, string> Errors { get; set; }

        /// <summary>
        /// Whether the transactions against the payload have been successful.
        /// This actually counts the number of items in the Errors list.
        /// </summary>
        public bool IsSuccess {
            get
            {
                return (isSuccess && Errors.Count == 0);
            }
            set
            {
                isSuccess = value;
            }
        }

        /// <summary>
        /// The list of non-error messages that have been added to the payload while working with it.
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// Several constructors exist for creating a payload.
        /// </summary>
        #region Constructors

        public Payload()
        {
            this.Errors = new Dictionary<string, string>();
            this.Messages = new List<string>();
        }

        public Payload(PayloadStatusCode statusCode, T data) 
            : this()
        {
            this.Code = statusCode;
            this.Data = data;
        }

        public Payload(PayloadStatusCode statusCode, T data, Dictionary<string, string> errors)
            : this(statusCode, data)
        {
            this.Errors = errors;
        }

        public Payload(PayloadStatusCode statusCode, T data, Dictionary<string, string> errors, List<string> messages) 
            : this(statusCode, data, errors)
        {
            this.Messages = messages;
        }

        public Payload(PayloadStatusCode statusCode, T data, string message)
            : this(statusCode, data)
        {
            this.Messages.Add(message);
        }

        #endregion
    }

    public enum PayloadStatusCode
    {
        /// <summary>
        /// When everything worked properly.
        /// </summary>
        Success = 1,

        /// <summary>
        /// When the overall activity worked properly, but there were warnings that should at least be looked at.
        /// </summary>
        SuccessWithWarnings = 2,

        /// <summary>
        /// When the process failed due to a technical error not business logic.
        /// </summary>
        TechnicalError = 3,

        /// <summary>
        /// When the process failed due to a business logic or validation error.
        /// </summary>
        ValidationError = 4,

        /// <summary>
        /// When the request was unallowed to be null.
        /// </summary>
        NullError = 5
    }
}