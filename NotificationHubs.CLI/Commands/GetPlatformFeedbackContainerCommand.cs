using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    public record GetPlatformFeedbackContainerCommand : CommandBase
    {
        protected override Task<int> Execute(NotificationHubClient nhClient)
        {
            var result = nhClient.GetFeedbackContainerUriAsync();
            WriteCommandResult(result);

            return 0;
        }
    }
}
