using CommandLine;
using Microsoft.Azure.NotificationHubs;
using System;

namespace NotificationHubs.Cli.Commands
{
    public abstract record SendCommand: HubLevelCommandBase
    {
        [Option("platform", Required = true)]
        public NotificationPlatform? Platform { get; set; }

        [Option("body", Required = true)]
        public string Body { get; set; }

        [Option("scheduled-time")]
        public DateTimeOffset? ScheduledTime { get; set; }

        protected Notification CreatePayload()
        {
            switch (Platform.Value)
            {
                case NotificationPlatform.Adm: return new AdmNotification(Body);
                case NotificationPlatform.Apns: return new AppleNotification(Body);
                case NotificationPlatform.Baidu: return new BaiduNotification(Body);
                case NotificationPlatform.Fcm: return new FcmNotification(Body);
                case NotificationPlatform.Mpns: return new MpnsNotification(Body);
                case NotificationPlatform.Wns: return new WindowsNotification(Body);

                default: throw new NotSupportedException($"Notifications for platform {Platform.Value} are not supported by the CLI"); 
            }
        }
    }
}
