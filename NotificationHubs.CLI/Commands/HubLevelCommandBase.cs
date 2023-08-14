using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    public abstract record HubLevelCommandBase : CommandBase
    {
        [Option]
        public string Hub { get; set; }

        protected override async Task<int> ExecuteAsync(HttpClient httpClient)
        {
            var nhClient = new NotificationHubClient(ConnectionString, Hub, new NotificationHubSettings
            {
                HttpClient = httpClient
            });

            return await ExecuteAsync(nhClient);
        }

        protected abstract Task<int> ExecuteAsync(NotificationHubClient nhClient);
    }
}
