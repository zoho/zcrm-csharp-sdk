
# Archival Notice:

This SDK is archived. You can continue to use it, but no new features or support requests will be accepted. For the new version, refer to
https://www.zoho.com/crm/developer/docs/sdk/server-side/csharp-sdk.html

# C# SDK for Zoho CRM
----------------------
C# SDK for Zoho CRM APIs provides wrapper for Zoho CRM APIs. Hence, invoking a Zoho CRM API from your client C# application is only a method call.

Registering a Zoho Client
-------------------------
Since Zoho CRM APIs are authenticated with OAuth2 standards, you should register your client app with Zoho. To register your app:
1) Visit this page [https://accounts.zoho.com/developerconsole](https://accounts.zoho.com/developerconsole).
2) Click on `Add Client ID`.
3) Enter `Client Name`, `Client Domain` and `Redirect URI`. 
4) Select the `Client Type` as `Web based`.
5) Click `Create`.

Your Client app would have been created and displayed by now.
The newly registered app's `Client ID` and `Client Secret` can be found by clicking `Options` → `Edit`.
(Options is the three dot icon at the right corner).

Setting Up
----------
C# SDK is available as a `Nuget` Package. The `ZCRMSDK` Assembly can be installed through `Nuget Package Manager` and through the following options:
Package Manager:

	>Install-Package ZCRMSDK --version 2.2.4/

.NET CLI:

	>dotnet add package ZCRMSDK --version 2.2.4/

>**Note:** The C# SDK is built against .net framework v4.6.1.

Configurations
--------------
>**Note:** From version 2.0.1 onwards configuration details has to be passed as a Dictionary.
```
public static Dictionary<string, string> config = new Dictionary<string, string>()
{
    {"client_id","1000.xxxxxxxxxxxxxxxxxxxxxxxxxxx"},
    {"client_secret","b47xxxxxxxxxxxxxxxxxxxxxxxxx"},
    {"redirect_uri","{redirect_url}"},
    {"access_type","offline"},
    {"persistence_handler_class","ZCRMSDK.OAuth.ClientApp.ZohoOAuthDBPersistence, ZCRMSDK"},
    {"oauth_tokens_file_path","{file_path}"},
    {"mysql_username","root"},
    {"mysql_password",""},
    {"mysql_database","zohooauth"},
    {"mysql_server","localhost"},
    {"mysql_port","3306"},
    {"apiBaseUrl","https://www.zohoapis.com"},
    {"iamURL","https://accounts.zoho.com"},
    {"fileUploadUrl","https://content.zohoapis.com"}
    {"photoUrl","{photo_url}"},
    {"apiVersion","v2"},
    {"logFilePath","{log_file_path}" },
    {"timeout",""},
    {"minLogLevel",""},
    {"domainSuffix",""},
    {"currentUserEmail","user@user.com"}
};
ZCRMRestClient.Initialize(config);

```

>**Note:**  For the version below 2.0.1 configuration details can be passed as app.config file. 

Your OAuth Client details should be given to the SDK as a section in app.config file. Add a section oauth_configuration in the app.config file and make sure that the section has the attribute type as `ZCRMSDK.CRM.Library.Common.ConfigFileHandler.ConfigFileSection, ZCRMSDK`. It is illustrated in the example below.
    
    <configuration>
        <configSections>
            <section name="oauth_configuration" type="ZCRMSDK.CRM.Library.Common.ConfigFileHandler.ConfigFileSection, ZCRMSDK"></section>
            <section name="zcrm_configuration" type="ZCRMSDK.CRM.Library.Common.ConfigFileHandler.ConfigFileSection, ZCRMSDK"></section>
        </configSections>    
            <oauth_configuration>
                <settings>
                    <add key = "client_id" value = "" />
                    <add key = "client_secret" value = "" />
                    <add key = "redirect_uri" value = "" />
                    <add key = "access_type" value = "offline"/>
                    <add key = "persistence_handler_class" value = ""/>
                    <add key = "oauth_tokens_file_path" value = ""/>
                    <add key = "mysql_username" value = "root"/>
                    <add key = "mysql_password" value = ""/>
                    <add key = "mysql_database" value = "zohooauth"/>
                    <add key = "mysql_server" value = "localhost"/>
                    <add key = "mysql_port" value = "3306"/>
                    <add key = "iamURL" value = ""/>
                </settings>
            </oauth_configuration>
            <zcrm_configuration>
                <settings>
                    <add key = "apiBaseUrl" value = ""/>
                    <add key = "photoUrl" value = ""/>
                    <add key = "apiVersion" value = "v2"/>
                    <add key = "loginAuthClass" value = ""/>
                    <add key = "logFilePath" value = ""/>
                    <add key = "timeout" value = ""/>
                    <add key = "minLogLevel" value = ""/>
                    <add key = "domainSuffix" value = ""/>
                    <add key = "currentUserEmail" value = "" />
                </settings>
            </zcrm_configuration>
      </configuration>
- `client_id`, `client_secret` and `redirect_uri` are your OAuth client’s configurations that you get after registering your Zoho client.
- `access_type` will be set to offline by default. Access and Refresh tokens will be received only when it is offline.
- `persistence_handler_class` is your implementation of the IZohoPersistenceHandler interface, which has handler methods to store OAuth data. This is discussed in the next section.
For example: `persistence_handler_class=ZCRMSDK.OAuth.ClientApp.ZohoOAuthFilePersistence, ZCRMSDK` (or) `ZCRMSDK.OAuth.ClientApp.ZohoOAuthDBPersistence, ZCRMSDK` (or) your own persistence handler class.
- If you prefer to use our DB persistence (`ZohoOAuthDBPersistence.cs`) , you need to give the `mysql_username` and `mysql_password` keys for mysql connectivity.
 
 	- By default, mysql_username = "root", mysql_password = "", mysql_database = "zohooauth", mysql_server = "localhost" and mysql_port = "3306".
  
	- The tokens are generated and placed in the database table automatically(which is explained in the ZohoOauthDBPersistence section) once the authentication process is complete.

- The `oauth_tokens_file_path` is required if the SDK's File Persistence is used as the persistence handler. It is the path of the file for storing the tokens of the user.

- `iamURL` - Url to be used when calling an Oauth accounts. It is used to denote the domain of the user. Url may be 
	- `https://accounts.zoho.com` for US.
	- `https://accounts.zoho.eu` for European countries.
	- `https://accounts.zoho.in` for India.
	- `https://accounts.zoho.com.cn` for China.

Other than the above OAuth configurations, the SDK also provides options to override certain HTTP request attributes. These configurations should be provided under a section named `zcrm_configuration`, in the app.config file.

The type of the section should be `ZCRMSDK.CRM.Library.Common.ConfigFileHandler.ConfigFileSection, ZCRMSDK`.

The following are the supported configurations in the zcrm_configuration section:

- `apiBaseUrl` - Url to be used when calling an API. It is used to denote the domain of the user. Url may be 
	- `https://www.zohoapis.com` for US.
	- `https://www.zohoapis.eu` for European countries.
	- `https://www.zohoapis.in` for India.
	- `https://www.zohoapis.com.cn` for China.
- `fileUploadUrl` - URL to be used when uploading the zip file.
	- `https://content.zohoapis.com` for US.
- `photoUrl` - Url for the image representing the record. The domain might be different based on the apiBaseUrl. Url may be 
	- `https://profile.zoho.com/api/v1/user/self/photo` for US.
	- `https://profile.zoho.eu/api/v1/user/self/photo` for European countries.
	- `https://profile.zoho.in/api/v1/user/self/photo` for India.
	- `https://profile.zoho.com.cn/api/v1/user/self/photo` for China.
- `apiVersion` is "v2".
- `timeOut` - Represents the request timeout in milliseconds. Let this be omitted or empty if not needed.
- `minLogLevel` - Represents the minimum log level for logging of SDK. The supported values are `ALL`, `INFO`, `WARNING`, `ERROR` and `OFF`. The default minimum log level is `WARNING`.
- `logFilePath` - Represents the file to which the SDK can log. Optional configuration and can be omitted. If omitted, the SDK logs the working in the execution directory of the application under the filename LogFile.log. Only the path of the file, without the file name, is needed for storing the logs.
- `currentUserEmail` - In case of single user, this configuration can be set. This user email is use fetch the corresponding access token from the persistance.
- `domainSuffix` - Optional configuraion. Provides Multi-DC Support. Ex: com, eu or cn.

	> **Note:** If the file path for "logFilePath" is not specified, then the "LogFile.log" is created in the "{Project}/bin/Debug/" folder of the project.

OAuth Persistence
----------
After the app is being authorized by the user, OAuth access and refresh tokens can be used for subsequent user data requests to Zoho CRM. Hence, they need to be persisted by the client app. 
To facilitate this, you should write an implementation of the `IZohoPersistenceHandler` interface, which has the following callback methods. 
- `SaveOAuthTokens(ZohoOAuthTokens tokens)` — Invoked while fetching access and refresh tokens from Zoho. Also when refreshing the access token, this method should get invoked.
- `DeleteOAuthTokens()`— Invoked before saving the newly received tokens. 
- `GetOAuthTokens()` — Invoked before firing a request to fetch the saved tokens. This method should return ZohoOAuthTokens object for the library to process it. 
Three sample implementations of `IZohoPersistenceHandler` are readily available with the client library. 
	- `ZohoOAuthFilePersistence`
	- `ZohoOAuthDBPersistence`
	- `ZohoOAuthInMemoryPersistence`

The name (along with its assembly as comma seperated) of the implemented class or the handlers provided by the sdk should be given as value for the key peristence_handler_class illustrated as `persistence_handler_class=<persistence_handler_class, assembly_name>` under the oauth_configuration section in the app.config file. By default, if the persistence handler class is not specified, InMemory Persistence handler handles the persistence implementation. 

> **Note:** Pre-defined persistence handler classes belong to the assembly ZCRMSDK.

#### ZohoOAuthDBPersistence

Uses a custom MySQL persistence. To use this, you should make sure of the following.  
* `MySQL` should be running in the same machine serving at the default port `3306`. 
* The database name should be `zohooauth`. 
* There must be a table `oauthtokens` with the columns `useridentifier (varchar(100)), accesstoken (varchar(100)), refreshtoken (varchar(100)) and expirytime (bigint)`.  
 
#### ZohoOAuthFilePersistence
Uses a local file to write and read the OAuth tokens. The full path of the file to be used by the library to write and read the tokens should be specified under the `oauth_configuration` section in `app.config` file as the value of the key `oauth_tokens_file_path`. 

#### ZohoOAuthInMemoryPersistence
Uses a singleton class to store and retrieve tokens. Default implementation and requires no external file.
Once your application is restarted the user token is destroyed. You can generate access token from new grant token.

> **Note:** ZohoOAuthFilePersistence and ZohoOAuthInMemoryPersistence implementations only support to store and refresh only a single user’s token. Hence these shall be used if the app accesses Zoho APIs on behalf of a single user only. In case if the app has to support for multiple users, Use ZohoOAuthDBPersistence or write your implementation of IZohoPersistenceHandler.

Initialization
---------
The app is ready to be initialized after defining OAuth configuration file and OAuth persistence handler class for your app.

#### Generating grant tokens
##### For Single user:
The developer console has an option to generate grant token for a user directly. This option may be handy when your app is going to use only one CRM user's credentials for all its operations or for your development testing.

1. Log into the User's account.
2. Visit [https://accounts.zoho.com/developerconsole](https://accounts.zoho.com/developerconsole).
3. Click on the Options → Self Client option of the client for which you wish to authorize.
4. Enter one or more (comma separated) valid Zoho CRM scopes that you wish to authorize in the Scope field and choose the time of expiry.
5. Copy the grant token that is displayed on the screen.

> **Note:** The generated grant token is valid only for the stipulated time you chose while generating it. Hence, the access and refresh tokens should be generated within that time.

The OAuth client registration and grant token generation must be done in the same Zoho account's (meaning - login) developer console.

##### For Multiple Users:
For multiple users, it is the responsibility of your client app to generate the grant token from the users trying to login. 
- Your Application's UI must have a `Login with Zoho` option to open the grant token URL of Zoho, which would prompt for the user's Zoho login credentials.
- Upon successful login of the user, the grant token will be sent as a param to your registered redirect URL.

#### Generating Access tokens
After obtaining the grant token, the following code snippet should be executed from your main class to get access and refresh tokens. Please paste the copied grant token in the string literally mentioned.  This is one time process only.
```
ZCRMRestClient.Initialize(config);
ZohoOAuthClient client = ZohoOAuthClient.GetInstance();
string grantToken = "Paste the generated Access Token here";
ZohoOAuthTokens tokens = client.GenerateAccessToken(grantToken);
string accessToken = tokens.AccessToken;
string refreshToken = tokens.RefreshToken;
```
In case of multiple users using the application, you need to keep note of the following:

- In order for the SDK to identify the particular user who made the request, the requester's email address should be given throught the following code snippet before making the actual method call of the SDK.
```
ZCRMRestClient.SetCurrentUser(“provide_current_user_email_here”)
```
In case of Single users, the current user email can be set either through the above code, or in the `zcrm_configuration` section in the `app.config` file with the key `currentUserEmail` as a one time configuration.

#### From refresh token
The following code snippet should be executed from your main class to get access token.
```
ZCRMRestClient.Initialize(config);
ZohoOAuthClient client = ZohoOAuthClient.GetInstance();
string refreshToken = <paste_refresh_token_here>;
string userMailId = <provide_user_email_here>;
ZohoOAuthTokens tokens = client. GenerateAccessTokenFromRefreshToken(refreshToken,userMailId);
```
Please paste the generated refresh token in the string literal mentioned. This is one time process only.

> Note:
	1.The above code snippet is valid only once per grant token. Upon its successful execution, the generated access and refresh tokens would have been persisted through your persistence handler class.
	2.Once the OAuth tokens have been persisted, subsequent API calls would use the persisted access and refresh tokens. The SDK will take care of refreshing the access token using refresh token, as and when required.

#### App Startup
The SDK requires the following line of code being invoked every time your app gets started.
```
ZCRMRestClient.Initialize(config); 
```

>**Note:** This method should be called from the main class of your c# application to start the application. It needs to be invoked without any exception.
>Once the SDK has been initialized, you can use any methods of the SDK to get proper results from the APIs.

Class Hierarchy
---------------
All Zoho CRM entities are modelled as classes having members and methods applicable to that particular entity. `ZCRMRestClient` is the base class of the SDK. `ZCRMRestClient` has methods to get instances of various other Zoho CRM entities.

The class relations and hierarchy of the SDK follows the entity hierarchy inside Zoho CRM. The class hierarchy of various Zoho CRM entities is given below:

    -ZCRMRestClient
     -ZCRMOrganization
       -ZCRMUser
       -ZCRMRole 
       -ZCRMProfile
     -ZCRMModule   
       -ZCRMLayout  
         -ZCRMSection    
           -ZCRMField         
           -ZCRMPickListValue    
       -ZCRMCustomView
		   -ZCRMTag 
       -ZCRMModuleRelation 
         -ZCRMJunctionRecord  
       -ZCRMRecord  
         -ZCRMInventoryLineItem  
           -ZCRMTax     
         -ZCRMPriceBookPricing 
         -ZCRMEventParticipant 
         -ZCRMNote      
         -ZCRMAttachment
       -ZCRMTrashRecord
       
Each class entity has functions to fetch it's own properties and to fetch data of its immediate child entities through an API call.

>For example: a Zoho CRM module (ZCRMModule) object will have member functions to get a module’s properties like display name, module Id, etc, and will also have functions to fetch all its child objects (like ZCRMLayout).

#### Instance object

- `ZCRMRestClient.GetModule("Contacts")` would return the actual Contacts module, that has all the properties of the Contacts module filled through an API call.
- `ZCRMRestClient.GetModuleInstance("Contacts")` would return a dummy `ZCRMModule` object that would refer to the Contacts module, with no properties filled, since this doesn’t make an API call.
Hence, to get records from a module, you need not start all the way from `ZCRMRestClient`. Instead, you could get a `ZCRMModule` instance with `ZCRMModule.GetInstance()` and then invoke its non-static GetRecords() method from the created instance. This would avoid the API call that would otherwise have been triggered to populate the `ZCRMModule` object.

#### Accessing record data
Since record properties are dynamic across modules, we have only given the common fields like `createdTime`, `createdBy`, owner etc. As `ZCRMRecord’s` default members. All other record properties are available as a dictionary in `ZCRMRecord` object.

- To access the individual field values of a record, use the properties available. The keys of the record properties dictionary are the API names of the module’s fields. API names of all fields of all modules are available under `Setup → Marketplace → APIs → CRM API → API Names`.

- To get a field value, use `record.GetFieldValue(fieldAPIName)`;
- To set a field value, use `record.SetFieldValue(fieldAPIName, newValue)`;

While setting a field value, please make sure of that the set value is of the apt data type of the field to which you are going to set it.

Response Handling
-----------------
`APIResponse`, `BulkAPIResponse` and `FileAPIResponse` are the wrapper objects for Zoho CRM APIs’ responses. All API calling methods would return one of these two objects.

- A method-seeking single entity would return `APIResponse` object, whereas a method-seeking list of entities would return `BulkAPIResponse` object.
- `FileAPIResponse` will be returned for file download APIs to download a photo or an attachment from a record or note such as `record.DownloadPhoto()`, `record.DownloadAttachment(Attachment_Id)` etc.
- Use the instance variable `Data` or `BulkData` property to get the entity data alone from the response wrapper objects. `APIResponse.Data` would return a single Zoho CRM entity object, while `BulkAPIResponse.BulkData` would return a list of Zoho CRM entity objects.
- `FileAPIResponse` has two defined methods namely `FileAPIResponse.GetFileName()` which returns the name of the file that is downloaded and `FileAPIResponse.GetFileAsStream()` that gives the file content as InputStream.

>**Note:** BulkAPIResponse is a generic class. Hence, to get the records, the corresponding type has to be used.
```
ZCRMModule module = ZCRMModule.GetInstance("Contacts");
BulkAPIResponse<ZCRMRecord> response = module.GetRecords();
List<ZCRMRecord> records = response.BulkData;
```
Other than data, these response wrapper objects have the following properties:

- `ResponseHeaders` - remaining API counts for the present day/window and time elapsed for the present window reset. It is available thorugh:
		
		 response.GetResponseHeaders()

- `ResponseInfo` - any other information, if provided by the API, in addition to the actual data.
	  
		 BulkAPIResponse<ZCRMRecord>.ResponseInfo info = response.Info;

- `List<EntityResponse>` - status of individual entities in a bulk API. For example: an insert records API may partially fail because of a few records. This dictionary gives the individual records’ creation status. It is available through:

		 response.BulkEntitiesResponse

Exceptions
-----------
All unexpected behaviors like faulty API responses, SDK anomalies are handled by the SDK and are converted and are thrown only as a single exception — `ZCRMException`. Hence, it's enough to catch this exception alone in the client app code.

    try
		{
			 // code block;
		}
		catch (ZCRMException ex)
		{
		    Console.WriteLine(ex.HttpStatusCode);
		    Console.WriteLine(ex.Code);
		    Console.WriteLine(ex.IsAPIException);
		    Console.WriteLine(ex.IsSDKException);
		    Console.WriteLine(ex.Message);
		    Console.WriteLine(ex.ErrorDetails);
		    Console.WriteLine(ex.ErrorMsg);
		}
Examples
---------
```
Sample code to insert a record:
-------------------------------
try{
    ZCRMRestClient.Initialize(config);
    ZCRMRestClient restClient = ZCRMRestClient.GetInstance();
    ZCRMRecord IRecord = new ZCRMRecord("Leads");#module API Name
	record.SetFieldValue(“field_api_name”, “field_value”);
    record.SetFieldValue(“field_api_name”, “field_value”);
    record.SetFieldValue(“field_api_name”, “field_value”);
    record.SetFieldValue(“field_api_name”, “field_value”);
    APIResponse userResponse = restClient.GetCurrentUser();
    ZCRMUser user = ZCRMUser.GetInstance(((ZCRMUser)userResponse.Data).Id);
    IRecord.Owner = user;
    List<ZCRMRecord> records = new List<ZCRMRecord> { IRecord };
    ZCRMModule module = restClient.GetModuleInstance(ApiName.Key);
    BulkAPIResponse<ZCRMRecord> response = module.CreateRecords(records);
    foreach (EntityResponse eResponse in response.BulkEntitiesResponse)
    {
	    Console.WriteLine(eResponse.ResponseJSON);//Fetches response as JSON
		Console.WriteLine(eResponse.Status);//Fetches status value present in the response
		Console.WriteLine(eResponse.Code);//Fetches code value present in the response
		Console.WriteLine(eResponse.Message);//Fetches message value present in the response
        }
}catch(ZCRMException ex){
	Console.WriteLine(ex.Message)
}
```
```
Sample code to fetch records:
-----------------------------
try{
	ZCRMRestClient.Initialize(config);
	ZCRMModule module = restClient.GetModuleInstance(ApiName.Key);
	BulkAPIResponse<ZCRMRecord> response = module.GetRecords();
	List<ZCRMRecord> records = response.BulkData;
	foreach(ZCRMRecord record in records)
	{
	 	Console.WriteLine(record.EntityId);//Fetches record id
	 	Console.WriteLine(record.Data);//Fetches a dictionary which has field api name and it’s value as key and value in that dictionary 
	 	Console.WriteLine(record.CreatedBy);//Fetches created by of the record
	 	Console.WriteLine(record.ModuleAPIName);//Fetches record’s module api name
	}
}catch(ZCRMException ex){
	Console.WriteLine(ex.Message)
}
```
    
