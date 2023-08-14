using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("cancel-notification", HelpText = "Cancels scheduled notification")]
    public record CancelNotificationAsync : HubLevelCommandBase
    {
        [Option("notification-id")]
        public string NotificationId { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            await nhClient.CancelNotificationAsync(NotificationId);

            return 0;
        }
    }
}
