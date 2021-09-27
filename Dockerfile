FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["backend_dockerAPI.csproj", "./"]
RUN dotnet restore "./backend_dockerAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "backend_dockerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backend_dockerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet backend_dockerAPI.dll