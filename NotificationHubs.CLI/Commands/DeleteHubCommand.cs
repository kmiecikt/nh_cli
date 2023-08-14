using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("delete-hub", HelpText = "Deletes a Notification Hub")]
    public record DeleteHubCommand : NamespaceLevelCommandBase
    {
        [Option("hub", Required = true)]
        public string Hub { get; set; }

        protected override async Task<int> ExecuteAsync(NamespaceManager nsManager)
        {
            await nsManager.DeleteNotificationHubAsync(Hub);

            return 0;
        }
    }
}
