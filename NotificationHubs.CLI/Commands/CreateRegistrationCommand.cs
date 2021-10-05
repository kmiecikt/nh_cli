using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("create-registration", HelpText = "Creates new registration")]
    public record CreateRegistrationCommand : CommandBase
    {
        [Option("pns-handle", Required = true)]
        public string PnsHandle { get; set; }

        [Option("tags")]
        public string Tags { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var tags = Tags?.Split(',') ?? Enumerable.Empty<string>();

            // TODO: support multiple platforms
            var registration = new FcmRegistrationDescription(PnsHandle, tags);

            var result = await nhClient.CreateRegistrationAsync(registration);
            WriteCommandResult(result);

            return 0;
        }
    }
}
