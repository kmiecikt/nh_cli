using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Utilities
{
    internal class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"* REQUEST: {request.Method} {request.RequestUri}");
            Console.WriteLine("* HEADERS:");

            foreach (var header in request.Headers)
            {
                Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (request.Content != null)
            {
                Console.WriteLine("* CONTENT:");
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
