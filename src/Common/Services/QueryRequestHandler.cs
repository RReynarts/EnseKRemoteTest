using System;
using System.Threading.Tasks;
using Common.Repository;

namespace Common.Services
{
    public abstract class QueryRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly IUnitOfWork _context;

        protected QueryRequestHandler(IUnitOfWork context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public Task<TResponse> HandleAsync(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Handle(request);
        }

        private async Task<TResponse> Handle(TRequest request)
        {
            using (_context.AsReadOnly())
            {
                return await QueryAsync(request);
            }
        }

        protected abstract Task<TResponse> QueryAsync(TRequest request);
    }
}
