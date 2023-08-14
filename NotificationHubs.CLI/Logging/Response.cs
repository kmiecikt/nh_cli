using System.Collections.Generic;
using System.Net;

namespace NotificationHubs.Cli.Logging
{
    public record Response(HttpStatusCode StatusCode, IDictionary<string, string> Headers, string Content)
    {
    }
}
