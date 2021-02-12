using NullGuard;
using System;

namespace TReuters.LogLoader.Infra.Crosscutting
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected Result(bool success, string error)
        {
            Contracts.Require(success || !string.IsNullOrEmpty(error));
            Contracts.Require(!success || string.IsNullOrEmpty(error));

            Success = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, String.Empty);
        }

        public static Result<T> Ok<T>([AllowNull] T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Ok();
        }
    }


    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            [return: AllowNull]
            get
            {
                Contracts.Require(Success || Error != null);

                return _value;
            }
            [param: AllowNull]
            private set { _value = value; }
        }

        protected internal Result([AllowNull] T value, bool success, string error)
            : base(success, error)
        {
            Contracts.Require(value != null || !success);

            Value = value;
        }
    }

    public static class Contracts
    {
        public static void Require(bool precondition, [AllowNull] string exceptionMessage = null)
        {
            if (!precondition)
                throw new ArgumentException(exceptionMessage);
        }
    }
}
