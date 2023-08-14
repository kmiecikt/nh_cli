using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-registrations-by-tag", HelpText = "Gets registrations by tag")]
    public record GetRegistrationsByTagCommand : HubLevelCommandBase
    {
        [Option("tag", Required = true)]
        public string Tag { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            // TODO: support count / pagination
            var registrations = await nhClient.GetRegistrationsByTagAsync(Tag, 100);
            WriteCommandResult(registrations);

            return 0;
        }
    }
}
