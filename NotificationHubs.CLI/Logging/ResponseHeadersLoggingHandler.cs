using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Logging
{
    internal class ResponseHeadersLoggingHandler : DelegatingHandler
    {
        private readonly ICliLogger _logger;

        public ResponseHeadersLoggingHandler(ICliLogger logger, HttpMessageHandler innerHandler)
           : base(innerHandler)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var result = new HttpResponseMessage(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var headers = new Dictionary<string, string>();

            result.Content = new StringContent(content);

            foreach (var header in response.Headers)
            {
                headers.Add(header.Key, string.Join(", ", header.Value));
                response.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            foreach (var header in response.Content.Headers)
            {
                headers.Add(header.Key, string.Join(", ", header.Value));
                result.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            foreach (var header in response.TrailingHeaders)
            {
                headers.Add(header.Key, string.Join(", ", header.Value));
                result.TrailingHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            var responseData = new Response(response.StatusCode, headers, content);
            _logger.LogResponse(responseData);

            return result;
        }
    }
}
