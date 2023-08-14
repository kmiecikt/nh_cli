using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-installation", HelpText = "Gets an installation")]
    public record GetInstallationCommand : HubLevelCommandBase
    {
        [Option("installation-id", Required = true)]
        public string InstallationId { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            var result = await nhClient.GetInstallationAsync(InstallationId);
            WriteCommandResult(result);

            return 0;
        }
    }
}
