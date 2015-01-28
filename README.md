OpenID-Connect-Authenticator
----------------------------

OpenID Connect authenticator for Agresso Business World Milestone 3 and later.

### Disclaimer

This authenticator is used at own risk. UNIT4 is not responsible for any damages caused by using 
this authenticator.

### Build

Requires these Agresso-libraries. 

* ACT.dll
* Agresso.Base.dll
* Agresso.Interface.Authentication.dll
* Agresso.Interface.CoreServices.dll

The project is created with Visual Studio 2012.

### Install

* Newtonsoft.Json.dll must be copied to binary folder
* Appsettings from app.config must be put into web.config
* Install with TAG107
* Set as default autenticator with TAG106

### Map users

The mapping will match users by looking for the OpenID email in the "Domain user" (User master file / TAG064 -> Security -> Single sign-on -> Domain user) field. The default client must be set.

### Limitations

* JWT Token signature is not verified. TLS required.

### Google setup

Follow instructions to [setup client](https://developers.google.com/accounts/docs/OAuth2Login?hl=no#appsetup)

### Further work

* When the OpenID Connect identity is not mapped to an Agresso user, there could be a feature that allows users to link accounts by entering Agresso credentials. This "Login-with-Agresso-user-to-link-accounts"-diaglog will only be displayed once per user to establish mapping between accounts.

### Troubleshoot

#### System will not load authenticator. Failed security check.

This error message is displayed because the system fails to compare the equality of assembly checksum of the executed assembly and the assembly checksum calculated when the authenticator was installed (TAG107).

##### Solution during development

Enter AG23 - Common parameters and locate the parameter VERIFY_AUTH_CHECKSUM. Turn it off. Then you won't have to update the checksum each time the authenticator is built (next solution).

##### Solution in production

Enter TAG107 - Authenticators and check the authenticator and click "Update Checksum". 

**Note**: Caches might have to be cleared in both cases!

### License

Copyright (c) 2014-2015 UNIT4

Released under the MIT license