using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("audience-send", HelpText = "Send notification to devices with matching tags")]
    public record AudienceSendCommand : SendCommand
    {
        [Option("tags", Required = true)]
        public string TagExpression { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            if (ScheduledTime != null)
            {
                var result = await nhClient.ScheduleNotificationAsync(CreatePayload(), ScheduledTime.Value, TagExpression);
                WriteCommandResult(result);
            }
            else
            {
                var result = await nhClient.SendNotificationAsync(CreatePayload(), TagExpression);
                WriteCommandResult(result);
            }

            return 0;
        }
    }
}
