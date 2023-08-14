using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-hub", HelpText = "Gets an existing Notification Hub")]
    public record GetHubCommand : NamespaceLevelCommandBase
    {
        [Option("hub", Required = true)]
        public string Hub { get; set; }

        protected override async Task<int> ExecuteAsync(NamespaceManager nsManager)
        {
            var hub = await nsManager.GetNotificationHubAsync(Hub);
            WriteCommandResult(hub);

            return 0;
        }
    }
}
