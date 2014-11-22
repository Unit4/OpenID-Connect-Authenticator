using Agresso.Foundation;
using Agresso.Interface.Authentication;
using Agresso.Interface.Authentication.SingleStage;
using Agresso.Interface.CoreServices;
using System;
using System.Web;

namespace OpenIdConnectAuthenticator
{
    [Authenticator("U4A_OPENID", "OpenID Connect-based authentication", false, PlatformConstant.Web)]
    public class OpenIdConnectAuthenticator : Authenticator
    {
        private IUsers _users;

        private IUsers Users
        {
            get
            {
                if (_users == null)
                {
                    _users = ObjectFactory.CreateInstance<IUsers>();
                }

                return _users;
            }
        }

        public override Response Authenticate(Credentials credentials)
        {
            Response response = new Response();
            
            string code = Request.QueryString["code"];

            if (!string.IsNullOrEmpty(code))
            {  
                if (Request.QueryString["state"] != HttpContext.Current.Session["state"].ToString())
                {
                    response.DenyAccess("Request is not valid. Please try again.");
                    return response;
                }

                string verifiedEmail = OAuth2.GetVerifiedEmail(code);
                if (string.IsNullOrEmpty(verifiedEmail))
                {
                    response.DenyAccess("Could complete authentication.");
                    return response;
                }

                IUserInfo user = Users.GetByDomainUser(verifiedEmail);
                if (user != null && Users.AllowedAccessClient(user.UserId, user.DefaultClient))
                {
                    response.GrantAccess(user.UserId, user.DefaultClient);
                    return response;
                }

                response.DenyAccess("User is not mapped");
                return response;
            }

            string state = RandState();

            string getCode = string.Format("{0}?response_type={1}&scope={2}&client_id={3}&redirect_uri={4}&state={5}", 
                Settings.AuthorizationEndpoint, 
                "code",
                "openid email", 
                Settings.ClientId, 
                Settings.RedirectUri, 
                state);

            HttpContext.Current.Session["state"] = state;
            HttpContext.Current.Response.Redirect(getCode);

            return response;
        }

        private string RandState()
        {
            string state = string.Empty;
            var random = new Random();

            for (int i = 0; i < 32; i++)
            {
                state += random.Next(9);
            }

            return state;
        }
    }

}
