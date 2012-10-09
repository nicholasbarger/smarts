using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// A structured format for payloads to be returned from REST calls in web api.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Payload<T>
    {
        /// <summary>
        /// Controls if manually set successful regardless of errors in collection.
        /// </summary>
        private bool? isSuccess = null;

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
                bool result = false;

                // Check if isSuccess is manually specified
                if (this.isSuccess.HasValue)
                {
                    result = this.isSuccess.Value && this.Errors.Count == 0;
                }
                // Check if not manually set relying on errors count
                else
                {
                    result = this.Errors.Count == 0;
                }

                return result;
            }
            set
            {
                this.isSuccess = value;
            }
        }

        /// <summary>
        /// The list of non-error messages that have been added to the payload while working with it.
        /// </summary>
        public List<string> Messages { get; set; }

        // Several constructors exist for creating a payload.
        #region Constructors

        public Payload()
        {
            this.Errors = new Dictionary<string, string>();
            this.Messages = new List<string>();
            this.Code = PayloadStatusCode.Unknown;
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

        // Methods (hopefully not too heavy logic in here)
        #region Helper Methods for Assigning Properties

        public void AssignExceptionErrors(Exception ex)
        {
            if (ex is DbEntityValidationException)
            {
                var sb = new StringBuilder();
                sb.Append(Resources.Errors.ERR00001 + Environment.NewLine + Environment.NewLine);
                foreach (var validationErrors in (ex as DbEntityValidationException).EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sb.Append(validationError.ErrorMessage + Environment.NewLine);
                    }
                }

                this.Errors.Add("00001", sb.ToString());
            }
            else if (ex is DbUpdateException || ex is SqlException)
            {
                this.Errors.Add("00001", ex.Message);
            }
            else if (ex is EntityCommandExecutionException)
            {
                this.Errors.Add("00001", ex.InnerException.Message);
            }
            else if (ex is UnauthorizedAccessException)
            {
                this.Errors.Add("00005", Resources.Errors.ERR00005);
            }
            else
            {
                this.Errors.Add("00000", ex.Message);
            }
        }

        public void AssignValidationErrors(Dictionary<string, string> valErrors)
        {
            // I've tried concat, union, and a few other methods and none are adding to error list properly
            // going back to brute force looping for now
            foreach (var error in valErrors)
            {
                this.Errors.Add(error.Key, error.Value);
            }
        }

        public void AssignDbErrors(DbEntityValidationException dbex)
        {
            foreach (var validationErrors in dbex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    this.Errors.Add("00001", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                }
            }
        }

        #endregion
    }

    public enum PayloadStatusCode
    {
        /// <summary>
        /// When the payload status is unassigned or unknown.
        /// </summary>
        Unknown = 0,

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