# guest-room

## EF Core

_From terminal_

* open project path of GuestRoom.Domain
* ensure 'Microsoft.EntityFrameworkCore.Tools' is installed via `Install-Package Microsoft.EntityFrameworkCore.Tools`
* ensure dotnet-ef is up to date with `dotnet tool update --global dotnet-ef --version 5.0.1`
* add migration like so `dotnet ef migrations add Initial -o Migrations -s ..\GuestRoom.Api\`
* alternatively cd into the GuestRoom.Api project and run `dotnet ef migrations add Initial -o Migrations -p ..\GuestRoom.Domain\`