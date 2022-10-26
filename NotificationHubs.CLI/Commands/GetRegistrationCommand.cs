using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-registration", HelpText = "Gets and existing registration")]
    public record GetRegistrationCommand : CommandBase
    {
        [Option("registration-id", Required = true)]
        public string RegistrationId { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            // TODO: support multiple platforms
            var result = await nhClient.GetRegistrationAsync<FcmRegistrationDescription>(RegistrationId);
            WriteCommandResult(result);

            return 0;
        }
    }
}
