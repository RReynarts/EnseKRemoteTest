using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Services
{
    internal class RequestHandlerMediator : IRequestHandlerMediator
    {
        internal const string InfoMessage = "Handled request for {0} ({1} ms).";
        private const string ErrorMessage = "Request for {0} caused an exception ({1} ms).";

        private readonly IServiceProvider _container;
        private readonly IRequestHandlerFactory _handlerFactory;
        private readonly ILogger<RequestHandlerMediator> _logger;

        public RequestHandlerMediator(IServiceProvider container, IRequestHandlerFactory handlerFactory,
            ILogger<RequestHandlerMediator> logger)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _handlerFactory = handlerFactory ?? throw new ArgumentNullException(nameof(handlerFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class where TResponse : class
        {
            var requestType = typeof(TRequest).FullName?.Replace("Ucb.Btp.Contracts.Requests.", "");
            var stopwatch = new Stopwatch();
            IRequestHandler<TRequest, TResponse> handler = null;

            using var scope = _container.CreateScope();
            try
            {
                stopwatch.Start();

                var validationErrors = new List<ValidationResult>();
                Validator.TryValidateObject(request, new ValidationContext(request), validationErrors, true);
                //if (validationErrors.Any())
                //    throw new Domain.Exceptions.ValidationException(
                //        Domain.Exceptions.ValidationException.ValidationErrors.Generic, "ContractValidation",
                //        validationErrors);

                //handler = _handlerFactory.Resolve<TRequest, TResponse>();
                handler = (IRequestHandler<TRequest, TResponse>) scope.ServiceProvider.GetService(
                    typeof(IRequestHandler<TRequest, TResponse>));
                var returnValue = await handler.HandleAsync(request);
                stopwatch.Stop();
                _logger.LogInformation(InfoMessage, requestType, stopwatch.ElapsedMilliseconds);
                return returnValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessage, requestType, stopwatch.ElapsedMilliseconds);
                throw;
            }
            finally
            {
                if (handler != null)
                {
                    _handlerFactory.Release(handler);
                }
            }
        }
    }
}