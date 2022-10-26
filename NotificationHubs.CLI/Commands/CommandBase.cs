using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
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

        [Option("ignore-certificate-errors")]
        public bool IgnoreCertificateErrors { get; set; }

        public async Task<int> Execute()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
                    ClientCertificateOptions = ClientCertificateOption.Manual
                };

                if (IgnoreCertificateErrors)
                {
                    handler.ServerCertificateCustomValidationCallback = (_, cert, _, _) =>
                    {
                        Console.WriteLine($"Skipping validation for certificate with SubjectName {cert.SubjectName.Name}");
                        return true;
                    };
                }

                var nhClient = new NotificationHubClient(ConnectionString, Hub, new NotificationHubSettings
                {
                    MessageHandler = handler
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

        protected void WriteCommandResult<T>(T result)
        {
            var json = JsonSerializer.Serialize(result);

            Console.WriteLine(json);
        }

        protected abstract Task<int> Execute(NotificationHubClient nhClient);
    }
}
