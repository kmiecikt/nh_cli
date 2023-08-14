using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("delete-registration-by-channel", HelpText = "Deletes registrations with given channel ID (PNS handle)")]
    public record DeleteRegistrationsByChannelCommand : HubLevelCommandBase
    {
        [Option("pns-handle", Required = true)]
        public string PnsHandle { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            await nhClient.DeleteRegistrationsByChannelAsync(PnsHandle);

            return 0;
        }
    }
}
