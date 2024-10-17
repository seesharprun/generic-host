# Sample ASP.NET Blazor application using a hosted service

1. `cd` to the */web* folder.

1. Set .NET user secret values using:

  - `dotnet user-secrets set "Credentials:Endpoint" "<azure-cosmos-db-nosql-account-endpoint>"`
  - `dotnet user-secrets set "Credentials:ReadWriteKey" "<azure-cosmos-db-nosql-account-read-write-key>"`

1. Run it with `dotnet watch run`.
