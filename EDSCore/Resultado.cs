namespace EDSCore
{
    public readonly struct Resultado<TValue, TError>
    {
        private readonly TValue? _value;
        private readonly TError? _error;

        private Resultado(TValue value)
        {
            IsError = false;
            _value = value;
            _error = default;
        }

        private Resultado(TError error)
        {
            IsError = true;
            _value = default;
            _error = error;
        }

        public bool IsError { get; }

        public bool IsSuccess => !IsError;

        public static implicit operator Resultado<TValue, TError>(TValue value) => new(value);
        public static implicit operator Resultado<TValue, TError>(TError error) => new(error);

        public TResult Match<TResult>(
            Func<TValue, TResult> sucess,
            Func<TError, TResult> failure) =>
            !IsError ? sucess(_value!) : failure(_error!);


    }
}
