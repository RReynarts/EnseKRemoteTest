using System;
using System.Threading.Tasks;
using Common.Repository;

namespace Common.Services
{
    public abstract class CommandRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly IUnitOfWork _context;
        private readonly bool _isLongRunning;

        protected IUnitOfWork Context => _context;
        protected CommandRequestHandler(IUnitOfWork context) : this(context, false) { }
        protected CommandRequestHandler(IUnitOfWork context, bool isLongRunning)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _isLongRunning = isLongRunning;
        }

        public async Task<TResponse> HandleAsync(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            using var scope = _context.BeginTransaction(_isLongRunning);
            var returnValue = await Execute(request);

            await scope.CompletedAsync();
            return returnValue;
        }

        protected abstract Task<TResponse> Execute(TRequest request);
    }
}
