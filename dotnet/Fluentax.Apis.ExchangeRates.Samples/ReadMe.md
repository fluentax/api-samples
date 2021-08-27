Run the following commands in your project directory to set up the credentials:
````powershell
dotnet user-secrets init
dotnet user-secrets set "Auth:ClientId" "[YourClientId]"
dotnet user-secrets set "Auth:ClientSecret" "[YourClientSecret]"