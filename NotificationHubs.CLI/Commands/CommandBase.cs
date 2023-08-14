using CommandLine;
using Microsoft.Azure.NotificationHubs;
using NotificationHubs.Cli.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    public abstract record CommandBase
    {
        [Option("connection-string")]
        public string ConnectionString { get; set; }

        [Option("log-request")]
        public bool LogRequest { get; set; }

        [Option("log-response")]
        public bool LogResponse { get; set; }

        [Option("log-format", Default = Logging.LogFormat.Text)]
        public LogFormat LogFormat { get; set; }

        [Option("custom-headers", Separator = ',')]
        public IList<string> CustomHeaders { get; set; }

        public async Task<int> ExecuteAsync()
        {
            using var logger = CreateLogger();

            try
            {
                var httpClient = CreateHttpClient(logger);

                return await ExecuteAsync(httpClient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while executing the command. Used options: {Environment.NewLine}    {this}{Environment.NewLine}Exception:");
                Console.Error.WriteLine(ex.ToString());

                return 1;
            }
        }

        protected abstract Task<int> ExecuteAsync(HttpClient httpClient);

        protected ICliLogger CreateLogger()
        {
            return LogFormat == Logging.LogFormat.Text
                ? new TextLogger()
                : new JsonLogger();
        }

        protected HttpClient CreateHttpClient(ICliLogger logger)
        {
            HttpMessageHandler handler = new HttpClientHandler
            {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                MaxConnectionsPerServer = 100
            };

            if (LogRequest)
            {
                handler = new RequestLoggingHandler(logger, handler);
            }

            if (LogResponse)
            {
                handler = new ResponseHeadersLoggingHandler(logger, handler);
            }

            var httpClient = new HttpClient(handler, true);

            if (CustomHeaders != null)
            {
                foreach (var header in CustomHeaders)
                {
                    var keyValue = header.Split(':');
                    if (keyValue.Length != 2)
                        throw new ArgumentException($"Invalid custom header: {header}. Headers must be passed in format key1:value1,key2:value2", nameof(CustomHeaders));
                    httpClient.DefaultRequestHeaders.Add(keyValue[0], keyValue[1]);
                }
            }

            return httpClient;
        }

        protected void WriteCommandResult<T>(T result)
        {
            var json = JsonSerializer.Serialize(result);

            Console.WriteLine(json);
        }
    }
}
