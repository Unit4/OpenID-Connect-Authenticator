OpenID-Connect-Authenticator
============================

Note: Alpha version. Not tested, do not use.

Build
-----

Requires these Agresso-libraries

* ACT.dll
* Agresso.Base.dll
* Agresso.Interface.Authentication.dll
* Agresso.Interface.CoreServices.dll

Install
-------

* Newtonsoft.Json.dll must be copied to binary folder
* Appsettings from app.config must be put into web.config
* Install with TAG107
* Set as default autenticator with TAG106


Limitations
-----------

* JWT Token signature is not verified. TLS required.

Google setup
------------

Follow instructions to [setup client](https://developers.google.com/accounts/docs/OAuth2Login?hl=no#appsetup)