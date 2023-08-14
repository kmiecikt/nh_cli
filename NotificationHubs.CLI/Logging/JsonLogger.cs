using System;
using System.Text.Json;

namespace NotificationHubs.Cli.Logging
{
    internal class JsonLogger : ICliLogger
    {
        private Request _request;
        private Response _response;

        public void LogRequest(Request request)
        {
            _request = request;
        }

        public void LogResponse(Response response)
        {
            _response = response;
        }

        public void Dispose()
        {
            if (_request == null && _response == null)
            {
                return;
            }

            var json = new
            {
                Request = _request,
                Response = _response,
            };

            Console.WriteLine();
            Console.WriteLine("* DEBUG LOGS *");

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
            };
            Console.WriteLine(JsonSerializer.Serialize(json, serializerOptions));

            _request = null;
            _response = null;
        }
    }
}
