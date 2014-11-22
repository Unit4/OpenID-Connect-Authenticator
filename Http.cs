using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace OpenIdConnectAuthenticator
{
    static class Http
    {
        public static string Post(string action, NameValueCollection form)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                byte[] r = webClient.UploadValues(action, form);

                return Encoding.UTF8.GetString(r);
            }
        }
    }
}
