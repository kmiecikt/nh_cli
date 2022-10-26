using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-registrations", HelpText = "Gets all registrations")]
    public record GetRegistrationsCommand : CommandBase
    {
        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            // TODO: support top / pagination
            var result = await nhClient.GetAllRegistrationsAsync(100);
            WriteCommandResult(result);

            return 0;
        }
    }
}
