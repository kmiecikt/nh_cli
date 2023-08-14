using Microsoft.Azure.NotificationHubs;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    public abstract record NamespaceLevelCommandBase : CommandBase
    {
        protected override async Task<int> ExecuteAsync(HttpClient httpClient)
        {
            var nsManager = new NamespaceManager(ConnectionString, new NotificationHubSettings
            {
                HttpClient = httpClient
            });

            return await ExecuteAsync(nsManager);
        }

        protected abstract Task<int> ExecuteAsync(NamespaceManager nsManager);
    }
}
