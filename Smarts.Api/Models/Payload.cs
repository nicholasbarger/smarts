using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    public class Payload<T>
    {
        private bool isSuccess = false;

        public PayloadStatusCode Code { get; set; }
        public T Data { get; set; }
        public Dictionary<string, string> Errors { get; set; }
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
        public List<string> Messages { get; set; }

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
    }

    public enum PayloadStatusCode
    {
        Success = 1,
        SuccessWithWarnings = 2,
        TechnicalError = 3,
        ValidationError = 4,
        NullError = 5
    }
}