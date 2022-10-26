using CommandLine;
using Microsoft.Azure.NotificationHubs;
using NotificationHubs.CLI;
using System;

namespace NotificationHubs.Cli.Commands
{
    public abstract record SendCommand: CommandBase
    {
        [Option("pns-type")]
        public PnsType PnsType { get; set; }

        [Option("body")]
        public string Body { get; set; }

        [Option("scheduled-time")]
        public DateTimeOffset? ScheduledTime { get; set; }

        protected Notification CreatePayload()
        {
            if (PnsType == PnsType.NotSet)
                throw new ArgumentException("Parameters \"pns-type\" is required", "pns-type");

            switch (PnsType)
            {
                case PnsType.Adm: return new AdmNotification(Body);
                case PnsType.Apple: return new AppleNotification(Body);
                case PnsType.Baidu: return new BaiduNotification(Body);
                case PnsType.Fcm: return new FcmNotification(Body);
                case PnsType.Mpns: return new MpnsNotification(Body);
                case PnsType.Windows: return new WindowsNotification(Body);

                default: throw new NotSupportedException($"Notifications for PNS type {PnsType} are not supported by the CLI"); 
            }
        }
    }
}
