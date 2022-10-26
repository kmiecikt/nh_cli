using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationHubs.Cli.Commands
{
    [Verb("create-registration", HelpText = "Creates new registration")]
    public record CreateRegistrationCommand : CommandBase
    {
        [Option("pns-handle", Required = true)]
        public string PnsHandle { get; set; }

        [Option("platform", Required = true)]
        public NotificationPlatform Platform { get; set; }

        [Option("tags")]
        public string Tags { get; set; }

        protected override async Task<int> Execute(NotificationHubClient nhClient)
        {
            var tags = Tags?.Split(',') ?? Enumerable.Empty<string>();

            var registration = CreateRegistrationDescription(Platform, PnsHandle, tags);

            var result = await nhClient.CreateRegistrationAsync(registration);
            WriteCommandResult(result);

            return 0;
        }

        private static RegistrationDescription CreateRegistrationDescription(NotificationPlatform platform, string pnsHandle, IEnumerable<string> tags)
        {
            switch (platform)
            {
                case NotificationPlatform.Adm:
                    return new AdmRegistrationDescription(pnsHandle, tags);

                case NotificationPlatform.Apns:
                    return new AppleRegistrationDescription(pnsHandle, tags);

                case NotificationPlatform.Fcm:
                    return new FcmRegistrationDescription(pnsHandle, tags);

                case NotificationPlatform.Baidu:
                    return new BaiduRegistrationDescription(pnsHandle) { Tags = new HashSet<string>(tags) };

                case NotificationPlatform.Mpns:
                    return new MpnsRegistrationDescription(pnsHandle, tags);

                case NotificationPlatform.Wns:
                    return new WindowsRegistrationDescription(pnsHandle, tags);

                default:
                    throw new NotSupportedException($"Platform {platform} is not supported by the CLI");
                
            }
        }
    }
}
