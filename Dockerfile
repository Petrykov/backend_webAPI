FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
COPY . .

CMD /bin/sh -c ASPNETCORE_URLS\=http://\*:\$PORT\ dotnet\ backend_dockerAPI