FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS runtime
WORKDIR /app
COPY /out ./

ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet","backend_dockerAPI.dll"]