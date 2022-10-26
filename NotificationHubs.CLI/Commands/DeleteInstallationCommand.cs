using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("delete-installation", HelpText = "Deletes an installation")]
    public record DeleteInstallationCommand : CommandBase
    {
        [Option("installation-id", Required = true)]
        public string InstallationId { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            await nhClient.DeleteInstallationAsync(InstallationId);

            return 0;
        }
    }
}
