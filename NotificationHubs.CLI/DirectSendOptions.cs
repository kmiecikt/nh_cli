using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.CLI
{
    [Verb("direct-send", HelpText = "Send notification to single device")]
    public record DirectSendOptions: OptionsBase
    {
        [Option]
        public string Body { get; set; }

        [Option("device-handle")]
        public string DeviceHandle { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            await nhClient.SendDirectNotificationAsync(new FcmNotification(Body), DeviceHandle);

            return 0;
        }
    }
}
