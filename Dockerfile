FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["backend_dockerAPI/backend_dockerAPI.csproj", "backend_dockerAPI/"]
RUN dotnet restore "backend_dockerAPI/backend_dockerAPI.csproj"
COPY . .
WORKDIR "/src/backend_dockerAPI"
RUN dotnet build "backend_dockerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backend_dockerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backend_dockerAPI.dll"]