using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.CLI
{
    [Verb("broadcast-send", HelpText = "Send notification to all devices")]
    public record BroadcastSendOptions: OptionsBase
    {
        [Option(ResourceType = typeof(string))]
        public string Body { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            await nhClient.SendNotificationAsync(new FcmNotification(Body));

            return 0;
        }
    }
}
