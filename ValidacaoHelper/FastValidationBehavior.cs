using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidacaoHelper.Notification;

namespace ValidacaoHelper
{

    public sealed class FastValidationBehavior<TRequest, Unit> : IPipelineBehavior<TRequest, Unit>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IDomainNotificationContext _notificationContext;

        public FastValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IDomainNotificationContext notificationContext)
        {
            _validators = validators;
            _notificationContext = notificationContext;
        }

        public Task<Unit> Handle(TRequest request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
        {

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any() ? Notify(failures) : next();
        }

        private Task<Unit> Notify(IEnumerable<ValidationFailure> failures)
        {
            var result = default(Unit);

            foreach (var failure in failures)
            {
                _notificationContext.NotifyError(failure.ErrorMessage);
            }

            return Task.FromResult(result);
        }

    }
}
