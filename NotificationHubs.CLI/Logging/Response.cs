using System.Collections.Generic;
using System.Net;

namespace NotificationHubs.Cli.Logging
{
    internal record Response(HttpStatusCode StatusCode, IDictionary<string, string> Headers, string Content)
    {
    }
}
