using System;
namespace MShare.Framework.Types
{
    public class Result
    {
        public bool IsSuccess { get; private init; }
        public string? FailMessage { get; private init; }
        public bool IsFail => !IsSuccess;

        protected Result(bool isSuccess, string? failMessage)
        {
            IsSuccess = isSuccess;
            FailMessage = failMessage;
        }

        public static Result Fail(string? failMessage = null)
            => new Result(isSuccess: false, failMessage);

        public static Result Success()
            => new Result(isSuccess: true, failMessage: null);
    }

    public class Result<TResult> : Result
    {
        public TResult? Data { get; init; }

        protected Result(bool isSuccess, string? failMessage = null, TResult? data = default(TResult)) : base(isSuccess, failMessage)
        {
            Data = data;
        }

        public static new Result<TResult> Fail(string? failMessage = null)
            => new Result<TResult>(isSuccess: false, failMessage);

        public static Result<TResult> Success(TResult? data)
            => new Result<TResult>(isSuccess: true, failMessage: null, data);
    }

    public class HttpResult<TResult> : Result<TResult>
    {
        public int StatusCode { get; init; }
        public bool IsUnauthorized => StatusCode == 401;
        public bool IsBadRequest => StatusCode == 400;
        public bool IsInternalServerError => StatusCode == 500;

        private HttpResult(int statusCode, string? failMessage = null, TResult? data = default)
            : base(statusCode <= 200 && statusCode < 400, failMessage, data)
        {
        }

        public static HttpResult<TResult> Ok(TResult? data = default)
            => new HttpResult<TResult>(statusCode: 200, failMessage: null, data);

        public static HttpResult<TResult> Unauthorized()
            => new HttpResult<TResult>(statusCode: 401, failMessage: nameof(Unauthorized));

        public static HttpResult<TResult> BadRequest()
            => new HttpResult<TResult>(statusCode: 400, failMessage: nameof(BadRequest));

        public static HttpResult<TResult> FromStatusCode(int statusCode, string? message = default)
            => new HttpResult<TResult>(statusCode, message);
    }

}

