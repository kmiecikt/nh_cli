using CommandLine;
using NotificationHubs.Cli.Commands;
using System.Threading.Tasks;

namespace NotificationHubs.Cli
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default
                .ParseArguments(args,
                    typeof(DirectSendCommand), 
                    typeof(BroadcastSendCommand), 
                    typeof(AudienceSendCommand), 
                    typeof(CancelNotificationAsync),
                    typeof(CreateRegistrationCommand),
                    typeof(UpsertRegistrationCommand),
                    typeof(UpdateRegistrationCommand),
                    typeof(CreateRegistrationIdCommand),
                    typeof(GetRegistrationCommand),
                    typeof(GetRegistrationsByTagCommand),
                    typeof(GetRegistrationsCommand),
                    typeof(GetRegistrationsByChannelCommand),
                    typeof(UpsertInstallationCommand),
                    typeof(GetInstallationCommand),
                    typeof(DeleteInstallationCommand),
                    typeof(PatchInstallationCommand),
                    typeof(DeleteRegistrationsByChannelCommand),
                    typeof(DeleteRegistrationCommand),
                    typeof(GetFeedbackContainerCommand),
                    typeof(GetNotificationOutcomeCommand),
                    typeof(SubmitJobCommand),
                    typeof(GetJobCommand),
                    typeof(GetJobsCommand),
                    typeof(GetHubCommand),
                    typeof(GetHubsCommand),
                    typeof(DeleteHubCommand))
                .MapResult(
                    (CommandBase options) => options.ExecuteAsync(),
                    _ => WriteUsage()); 
        }

        private static Task<int> WriteUsage()
        {
            return Task.FromResult(-1);
        }
    }
}
