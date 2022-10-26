using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("direct-send", HelpText = "Send notification to single device")]
    public record DirectSendCommand: SendCommand
    {
        [Option("device-handle", Required = true)]
        public string DeviceHandle { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            if (ScheduledTime != null)
                throw new NotSupportedException("Scheduled notifications are not supported for Direct Send");

            var result = await nhClient.SendDirectNotificationAsync(CreatePayload(), DeviceHandle);
            WriteCommandResult(result);

            return 0;
        }
    }
}
