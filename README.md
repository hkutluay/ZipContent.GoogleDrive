# ZipContent.GoogleDrive
Lists zip file content on Google Drive service without downloading whole document. Supports both zip and zip64 files.

# Usage

First install ZipContent.GoogleDrive via NuGet console:
```
PM> Install-Package ZipContent.GoogleDrive
```

Sample usage:
```csharp

string[] Scopes = { DriveService.Scope.Drive };
string ApplicationName = "ZipContent.GoogleDrive Sample";

UserCredential credential;

using (var stream = new FileStream(@"C:\temp\credentials.json", FileMode.Open, FileAccess.Read))
{
    // The file token.json stores the user's access and refresh tokens, and is created
    // automatically when the authorization flow completes for the first time.
    string credPath = @"C:\temp\token.json";
    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
}

// Create Drive API service.
var service = new DriveService(new BaseClientService.Initializer()
{
    HttpClientInitializer = credential,
    ApplicationName = ApplicationName,
});

// Define parameters of request.
FilesResource.ListRequest listRequest = service.Files.List();
listRequest.PageSize = 10;
listRequest.Q = "name contains 'zip'";

// List files.
IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

if (files != null && files.Count > 0)
{
    foreach (var file in files)
    {
                        
        var googleDrivePartialFileReader = new GoogleDrivePartialFileReader(service, file.Id);
        var zipContentLister = new ZipContent.Core.ZipContentLister();
        var ziplist = await list.GetContents(googleDrivePartialFileReader);
    }
}
 ```
