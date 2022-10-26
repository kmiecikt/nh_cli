using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-registrations-by-channel", HelpText = "Gets registrations by channel (PNS handle)")]
    public record GetRegistrationsByChannelCommand : CommandBase
    {
        [Option("pns-handle", Required = true)] 
        public string PnsHandle { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var result = await nhClient.GetRegistrationsByChannelAsync(PnsHandle, 100);
            WriteCommandResult(result);

            return 0;
        }
    }
}
