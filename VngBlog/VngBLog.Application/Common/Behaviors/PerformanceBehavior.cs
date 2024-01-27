using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace VngBLog.Application.Common.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            //_timer = Stopwatch.StartNew();
            _timer = _timer ?? new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();   //Mỗi lần có request là đồng hồ tự động chạy
            var response = await next();
            _timer.Stop();

            var ellapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (ellapsedMilliseconds <= 500)
            {
                return response;
            }
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Application Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, ellapsedMilliseconds, request);

            return response;
        }
    }
}
