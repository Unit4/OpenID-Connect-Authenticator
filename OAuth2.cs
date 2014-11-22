using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OpenIdConnectAuthenticator
{
    public class OAuth2
    {
        public static string GetVerifiedEmail(string code)
        {
            string responseJson = Http.Post(Settings.TokenEndpoint,
                new NameValueCollection()
                {
                    { "code", code },
                    { "client_id", Settings.ClientId },
                    { "client_secret", Settings.ClientSecret },
                    { "redirect_uri", Settings.RedirectUri },
                    { "grant_type", "authorization_code" },
                });

            dynamic response = JObject.Parse(responseJson);
            string idToken = response.id_token;
            string[] idArray = idToken.Split('.');

            dynamic idBody = JObject.Parse(Encoding.UTF8.GetString(Base64UrlDecode(idArray[1])));
            string issuer = idBody.iss;  
            string audience = idBody.aud;
            string issueTime = idBody.iat;
            int expires = idBody.exp;

            bool emailVerified = idBody.email_verified;
            string email = idBody.email;

            if (emailVerified && VerifyId(issuer, audience, expires))
            {
                return email;
            }

            return string.Empty;
        }

        private static bool VerifyId(string issuer, string audience, int expires)
        {
            if (!new Uri(Settings.Issuer).Host.Equals(issuer, StringComparison.InvariantCulture))
                return false;

            if (audience != Settings.ClientId)
                return false;

            if (expires < EpochNow)
                return false;

            return true;
        }

        private static int EpochNow
        {
            get
            {
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                return (int)t.TotalSeconds;
            }
        }

        // JWT spec
        public static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new System.Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }
    }
}
