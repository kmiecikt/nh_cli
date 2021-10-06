using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("patch-installation", HelpText = "Updates an existing installation")]
    public record PatchInstallationCommand : CommandBase
    {
        [Option("installation-id", Required = true)]
        public string InstallationId { get; set; }

        [Option("pns-handle")]
        public string PnsHandle { get; set; }

        [Option("user-id")]
        public string UserId { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var updateOperations = new List<PartialUpdateOperation>();
            if (PnsHandle != null)
            {
                updateOperations.Add(new PartialUpdateOperation { Operation = UpdateOperationType.Replace, Path = "/pushChannel", Value = PnsHandle });
            }

            if (UserId != null)
            {
                updateOperations.Add(new PartialUpdateOperation { Operation = UpdateOperationType.Replace, Path = "/userId", Value = UserId });
            }

            await nhClient.PatchInstallationAsync(InstallationId, updateOperations);

            return 0;
        }
    }
}
