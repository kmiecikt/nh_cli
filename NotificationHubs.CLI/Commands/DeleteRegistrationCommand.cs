using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{

    [Verb("delete-registration", HelpText = "Deletes registration with given ID")]
    public record DeleteRegistrationCommand : HubLevelCommandBase
    {
        [Option("registration-id", Required = true)]
        public string RegistrationId { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            await nhClient.DeleteRegistrationAsync(RegistrationId);

            return 0;
        }
    }
}
