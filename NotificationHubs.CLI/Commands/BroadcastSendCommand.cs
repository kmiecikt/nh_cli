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
            if (ScheduledTime != null)
            {
                var result = await nhClient.ScheduleNotificationAsync(CreatePayload(), ScheduledTime.Value);
                WriteCommandResult(result);
            }
            else
            {
                var result = await nhClient.SendNotificationAsync(CreatePayload());
                WriteCommandResult(result);
            }

            return 0;
        }
    }
}
