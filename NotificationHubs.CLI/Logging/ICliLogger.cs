namespace NotificationHubs.Cli.Logging
{
    internal interface ICliLogger
    {
        void LogRequest(Request request);

        void LogResponse(Response response);

        void Flush();
    }
}
