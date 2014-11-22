OpenID-Connect-Authenticator
============================

Version
-------
Alpha version. Not tested, do not use. Works on Milestone 3 and later.

Build
-----

Requires these Agresso-libraries. 

* ACT.dll
* Agresso.Base.dll
* Agresso.Interface.Authentication.dll
* Agresso.Interface.CoreServices.dll

Project is created with Visual Studio 2012.

Install
-------

* Newtonsoft.Json.dll must be copied to binary folder
* Appsettings from app.config must be put into web.config
* Install with TAG107
* Set as default autenticator with TAG106

Map users
---------

The mapping will match users by looking for the OpenID email in the "Domain user" (User master file / TAG064 -> Security -> Single sign-on -> Domain user) field. The default client must be set.

Limitations
-----------

* JWT Token signature is not verified. TLS required.

Google setup
------------

Follow instructions to [setup client](https://developers.google.com/accounts/docs/OAuth2Login?hl=no#appsetup)