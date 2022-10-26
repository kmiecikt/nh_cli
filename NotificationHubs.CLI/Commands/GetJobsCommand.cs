using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-jobs", HelpText = "Gets an existing Notification Hub Jobs")]
    public record GetJobsCommand : CommandBase
    {
        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var result = await nhClient.GetNotificationHubJobsAsync();
            WriteCommandResult(result);

            return 0;
        }
    }
}
