using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Logging
{
    internal class RequestLoggingHandler : DelegatingHandler
    {
        private readonly ICliLogger _logger;

        public RequestLoggingHandler(ICliLogger logger, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var content = request.Content != null ? await request.Content.ReadAsStringAsync() : null;

            var requestData = new Request(
                request.Method.ToString(),
                request.RequestUri,
                request.Headers.ToDictionary(k => k.Key, v => string.Join(", ", v.Value)),
                content);

            _logger.LogRequest(requestData);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
