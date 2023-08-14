using System;

namespace NotificationHubs.Cli.Logging
{
    public interface ICliLogger: IDisposable
    {
        void LogRequest(Request request);

        void LogResponse(Response response);
    }
}
