using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("update-registration", HelpText = "Updates an existing registration")]
    public record UpdateRegistrationCommand : CommandBase
    {
        [Option("pns-handle", Required = true)]
        public string PnsHandle { get; set; }

        [Option("registration-id", Required = true)]
        public string RegistrationId { get; set; }

        [Option("tags")]
        public string Tags { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var tags = Tags?.Split(',') ?? Enumerable.Empty<string>();

            // TODO: support multiple platforms
            var existingRegistration = await nhClient.GetRegistrationAsync<FcmRegistrationDescription>(RegistrationId);

            var registration = new FcmRegistrationDescription(existingRegistration)
            {
                Tags = new HashSet<string>(tags),
                FcmRegistrationId = PnsHandle
            };

            var result = await nhClient.UpdateRegistrationAsync(registration);
            WriteCommandResult(result);

            return 0;
        }
    }
}
