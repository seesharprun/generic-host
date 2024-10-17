# Sample Generic Host Application with two services

1. `cd` to the */host* folder.

1. Run it with `dotnet run`.

1. Set different .NET user secret values using:

  - `dotnet user-secrets set "Configuration:FirstPingFrequency" "15"`
  - `dotnet user-secrets set "Configuration:SecondPingFrequency" "20"`
