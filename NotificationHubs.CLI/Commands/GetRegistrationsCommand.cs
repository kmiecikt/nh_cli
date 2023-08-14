using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("get-registrations", HelpText = "Gets all registrations")]
    public record GetRegistrationsCommand : CommandBase
    {
        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            const int batchSize = 200;
            var result = new List<RegistrationDescription>();
            string continuationToken = null;

            do
            {
                var registrations = await (continuationToken != null ? nhClient.GetAllRegistrationsAsync(continuationToken, batchSize) : nhClient.GetAllRegistrationsAsync(batchSize));

                result.AddRange(registrations.Where(r => r.ExpirationTime <= DateTimeOffset.UtcNow || r.ExpirationTime > DateTimeOffset.MaxValue - TimeSpan.FromDays(1)));
                continuationToken = registrations.ContinuationToken;
            }
            while (continuationToken != null && result.Count < 1000);

            // TODO: support top / pagination
            //var result = await nhClient.GetAllRegistrationsAsync(100);
            //WriteCommandResult($"ContinuationToken: {result.ContinuationToken}; ");
            WriteCommandResult(result);

            return 0;
        }
    }
}
