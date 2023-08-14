using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-hubs", HelpText = "Lists all Notification Hubs")]
    public record GetHubsCommand : NamespaceLevelCommandBase
    {
        protected override async Task<int> ExecuteAsync(NamespaceManager nsManager)
        {
            var hubs = await nsManager.GetNotificationHubsAsync();
            WriteCommandResult(hubs);

            return 0;
        }
    }
}
