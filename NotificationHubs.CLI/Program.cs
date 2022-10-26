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
                .ParseArguments<DirectSendCommand, BroadcastSendCommand, AudienceSendCommand>(args)
                .MapResult(
                    (CommandBase options) => options.Execute(),
                    _ => WriteUsage()
                 ); ;
        }

        private static Task<int> WriteUsage()
        {
            return Task.FromResult(-1);
        }
    }
}
