using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("cancel-notification", HelpText = "Cancels scheduled notification")]
    public record CancelNotificationAsync : CommandBase
    {
        [Option("notification-id")]
        public string NotificationId { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            await nhClient.CancelNotificationAsync(NotificationId);

            return 0;
        }
    }
}
