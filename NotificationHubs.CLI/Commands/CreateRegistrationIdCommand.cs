using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("create-registration-id", HelpText = "Creates new registration ID")]
    public record CreateRegistrationIdCommand : HubLevelCommandBase
    {
        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            var result = await nhClient.CreateRegistrationIdAsync();
            WriteCommandResult(result);

            return 0;
        }
    }
}
