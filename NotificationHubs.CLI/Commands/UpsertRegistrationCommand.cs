using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("upsert-registration", HelpText = "Creates or updates registration")]
    public record UpsertRegistrationCommand : CommandBase
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
            var registration = new FcmRegistrationDescription(PnsHandle, tags)
            {
                RegistrationId = RegistrationId
            };

            var result = await nhClient.CreateOrUpdateRegistrationAsync(registration);
            WriteCommandResult(result);

            return 0;
        }
    }
}
