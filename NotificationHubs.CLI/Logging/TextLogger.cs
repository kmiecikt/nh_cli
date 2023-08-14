using System;

namespace NotificationHubs.Cli.Logging
{
    internal class TextLogger : ICliLogger
    {
        private Request _request;
        private Response _response;

        public void Dispose()
        {
            if (_request == null && _response == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("* DEBUG LOGS *");
            if (_request != null)
            {
                WriteRequest(_request);
            }

            if (_response != null)
            {
                WriteResponse(_response);
            }

            _request = null;
            _response = null;
        }

        private void WriteRequest(Request request)
        {
            Console.WriteLine();
            Console.WriteLine("* REQUEST LOG BEGIN *");
            Console.WriteLine($"{request.Method} {request.RequestUri}");

            Console.WriteLine();
            Console.WriteLine("** HEADERS **");

            foreach (var header in request.Headers)
            {
                Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (request.Content != null)
            {
                Console.WriteLine();
                Console.WriteLine("** CONTENT **");
                Console.WriteLine(request.Content);
            }

            Console.WriteLine("* REQUEST LOG END *");
            Console.WriteLine();
        }

        private void WriteResponse(Response response)
        {
            Console.WriteLine("* RESPONSE LOG BEGIN *");
            Console.WriteLine("** HEADERS **");
            Console.WriteLine($"  StatusCode: {(int)response.StatusCode} {response.StatusCode}");

            foreach (var header in response.Headers)
            {
                Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
            }

            Console.WriteLine("** CONTENT **");
            Console.WriteLine(response.Content);
            Console.WriteLine("* RESPONSE LOG END *");
        }

        public void LogRequest(Request request)
        {
            _request = request;
        }

        public void LogResponse(Response response)
        {
            _response = response;
        }
    }
}
