using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("submit-job", HelpText = "Create Notification Hub Job")]
    public record SubmitJobCommand : HubLevelCommandBase
    {
        [Option("job-type", Required = true)]
        public NotificationHubJobType JobType { get; set; }

        [Option("output-container-uri", Required = true)]
        public string OutputContainerUri { get; set; }

        [Option("input-file-uri")]
        public string ImportFielUri { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            var job = new NotificationHubJob
            {
                JobType = JobType,
                OutputContainerUri = new Uri(OutputContainerUri)
            };

            if (JobType != NotificationHubJobType.ExportRegistrations)
            {
                job.ImportFileUri = new Uri(ImportFielUri);
            }
            else if (ImportFielUri != null)
            {
                throw new ArgumentException("Option --input-file-uri cannot be used for Export job", nameof(ImportFielUri));
            }

            var result = await nhClient.SubmitNotificationHubJobAsync(job);
            WriteCommandResult(result);

            return 0;
        }
    }
}
