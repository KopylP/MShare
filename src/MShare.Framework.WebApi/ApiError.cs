using System;
using System.Text.Json.Serialization;

namespace MShare.Framework.WebApi
{
	public class ApiError
	{
        public int StatusCode { get; protected set; }

        public string StatusDescription { get; protected set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; protected set; }

        protected ApiError()
        {
        }

        public ApiError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
        }
    }
}

