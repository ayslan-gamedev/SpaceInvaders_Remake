using System;

namespace SpaceInvaders.Utilities
{
    public readonly struct Result<T, TE>
    {
        public readonly T Value;
        public readonly TE Error;

        private Result(T value, TE error, bool sucess)
        {
            Value = value;
            Error = error;
            IsOk = sucess;
        }

        public bool IsOk { get; }

        public static Result<T, TE> Ok(T value)
        {
            return new Result<T, TE>(value, default(TE), true);
        }

        public static Result<T, TE> Err(TE error)
        {
            return new Result<T, TE>(default(T), error, false);
        }

        public static implicit operator Result<T, TE>(T value) => new(value, default(TE), true);
        public static implicit operator Result<T, TE>(TE error) => new(default(T), error, false);

        public TR Match<TR>(
            Func<T, TR> success,
            Func<TE, TR> failure) =>
            IsOk ? success(Value) : failure(Error);
    }
}
