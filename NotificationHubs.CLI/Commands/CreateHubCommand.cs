using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("create-hub", HelpText = "Creates new Notification Hub")]
    public record CreateHubCommand : NamespaceLevelCommandBase
    {
        [Option("hub", Required = true)]
        public string Hub { get; set; }

        [Option("registration-ttl", Required = false)]
        public TimeSpan? RegistrationTtl { get; set; }

        protected override async Task<int> ExecuteAsync(NamespaceManager nsManager)
        {
            var hubDescription = new NotificationHubDescription(Hub);
            hubDescription.RegistrationTtl = RegistrationTtl;

            var result = await nsManager.CreateNotificationHubAsync(new NotificationHubDescription(Hub));
            WriteCommandResult(result);

            return 0;
        }
    }
}
