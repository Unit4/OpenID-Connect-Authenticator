using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace OpenIdConnectAuthenticator
{
    public static class Settings
    {
        public static string Issuer
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("openid:Issuer");
            }
        }

        public static string AuthorizationEndpoint
        {
            get
            {
                return Issuer + ConfigurationManager.AppSettings.Get("openid:Authorization");
            }
        }

        public static string TokenEndpoint
        {
            get
            {
                return Issuer + ConfigurationManager.AppSettings.Get("openid:Token");
            }
        }

        public static string ClientId
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("openid:ClientId");
            }
        }

        public static string ClientSecret
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("openid:ClientSecret");
            }
        }

        public static string RedirectUri
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("openid:RedirectUri");
            }
        }
    }
}
