using System;

namespace Common.Services
{
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IRequestHandler<TSource, TTarget> Resolve<TSource, TTarget>() where TSource : class where TTarget : class
        {
            return (IRequestHandler<TSource, TTarget>)_serviceProvider.GetService(typeof(IRequestHandler<TSource, TTarget>));
        }

        public void Release<TSource, TTarget>(IRequestHandler<TSource, TTarget> item) where TSource : class where TTarget : class
        {
            item = null;
        }
    }
}
