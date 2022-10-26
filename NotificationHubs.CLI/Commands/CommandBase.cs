using CommandLine;
using Microsoft.Azure.NotificationHubs;
using NotificationHubs.Cli.Utilities;
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

        [Option]
        public string Hub { get; set; }

        [Option("log-requests")]
        public bool LogRequests { get; set; }

        [Option("custom-headers", Separator = ',')]
        public IList<string> CustomHeaders { get; set; }

        public async Task<int> Execute()
        {
            try
            {
                var httpClient = CreateHttpClient();

                var nhClient = new NotificationHubClient(ConnectionString, Hub, new NotificationHubSettings
                {
                    HttpClient = httpClient
                });

                return await Execute(nhClient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while executing the command. Used options: {Environment.NewLine}    {this}{Environment.NewLine}Exception:");
                Console.Error.WriteLine(ex.ToString());

                return 1;
            }
        }

        private HttpClient CreateHttpClient()
        {
            HttpMessageHandler handler = new HttpClientHandler
            {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
                ClientCertificateOptions = ClientCertificateOption.Manual
            };

            if (LogRequests)
            {
                handler = new LoggingHandler(handler);
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

        protected abstract Task<int> Execute(NotificationHubClient nhClient);
    }
}
