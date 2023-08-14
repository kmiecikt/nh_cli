using System;
using System.Collections.Generic;

namespace NotificationHubs.Cli.Logging
{
    public record Request(
        string Method, Uri RequestUri, IDictionary<string, string> Headers, string Content)
    {
    }
}
