using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-feedback-container", HelpText = "Gets an Feedback Container URI")]
    public record GetFeedbackContainerCommand : HubLevelCommandBase
    {
        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            var result = await nhClient.GetFeedbackContainerUriAsync();
            WriteCommandResult(result);

            return 0;
        }
    }
}
