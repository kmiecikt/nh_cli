using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("direct-send", HelpText = "Send notification to single device")]
    public record DirectSendCommand: SendCommand
    {
        [Option("device-handle")]
        public string DeviceHandle { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            await nhClient.SendDirectNotificationAsync(CreatePayload(), DeviceHandle);

            return 0;
        }
    }
}
