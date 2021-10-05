using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("broadcast-send", HelpText = "Send notification to all devices")]
    public record BroadcastSendCommand: SendCommand
    {
        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            await nhClient.SendNotificationAsync(CreatePayload());

            return 0;
        }
    }
}
