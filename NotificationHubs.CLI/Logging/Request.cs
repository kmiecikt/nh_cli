using System;
using System.Collections.Generic;

namespace NotificationHubs.Cli.Logging
{
    internal record Request(
        string Method, Uri RequestUri, IDictionary<string, string> Headers, string Content)
    {
    }
}
