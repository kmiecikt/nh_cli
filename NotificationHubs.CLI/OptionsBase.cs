using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationHubs.CLI
{
    public abstract record OptionsBase
    {
        [Option("connection-string")]
        public string ConnectionString { get; set; }

        [Option]
        public string Hub { get; set; }

        public async Task<int> Execute()
        {
            try
            {
                var nhClient = new NotificationHubClient(ConnectionString, Hub, new NotificationHubSettings
                {
                    MessageHandler = new HttpClientHandler
                    {
                        SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
                        ClientCertificateOptions = ClientCertificateOption.Manual
                    }
                });
            
                return await Execute(nhClient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while sending notification. Used options: {Environment.NewLine}    {this}{Environment.NewLine}Exception:");
                Console.Error.WriteLine(ex.ToString());
                return 1;
            }
        }

        protected abstract Task<int> Execute(NotificationHubClient nhClient);
    }
}
