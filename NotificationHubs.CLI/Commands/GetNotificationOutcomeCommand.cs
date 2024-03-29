﻿using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-notification-outcome", HelpText = "Gets an notification outcome")]
    public record GetNotificationOutcomeCommand : HubLevelCommandBase
    {
        [Option("notification-id", Required = true)]
        public string NotificationId { get; set; }

        protected override async Task<int> ExecuteAsync(NotificationHubClient nhClient)
        {
            var result = await nhClient.GetNotificationOutcomeDetailsAsync(NotificationId);
            WriteCommandResult(result);

            return 0;
        }
    }
}
