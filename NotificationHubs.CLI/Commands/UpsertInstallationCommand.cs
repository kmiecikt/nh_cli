using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("upsert-installation", HelpText = "Creates or updates an installation")]
    public record UpsertInstallationCommand : CommandBase
    {
        [Option("installation-id", Required = true)]
        public string InstallationId { get; set; }

        [Option("pns-handle", Required = true)]
        public string PnsHandle { get; set; }

        [Option("platform", Required = true)]
        public NotificationPlatform? Platform { get; set; }

        [Option("tags")]
        public string Tags { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var tags = Tags?.Split(',') ?? null;

            var installation = new Installation
            {
                InstallationId = InstallationId,
                PushChannel = PnsHandle,
                Platform = Platform.Value,
                Tags = tags, 
            };

            await nhClient.CreateOrUpdateInstallationAsync(installation);

            return 0;
        }
    }
}
