using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-job", HelpText = "Gets an existing Notification Hub Job")]
    public record GetJobCommand : CommandBase
    {
        [Option("job-id", Required = true)]
        public string JobId { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var job = await nhClient.GetNotificationHubJobAsync(JobId);
            WriteCommandResult(job);

            return 0;
        }
    }
}
