# Sample Generic Host Application

1. Run it with `dotnet build` in the */host* folder.

1. Set different .NET user secret values using:

  - `dotnet user-secrets set "Messages:Greeting" "Hello, person!"`
  - `dotnet user-secrets set "ServiceConfiguration:PingFrequency" "5"`
