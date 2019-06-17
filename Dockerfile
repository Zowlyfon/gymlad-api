FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY gymlad-api/*.csproj .
RUN dotnet restore

COPY gymlad-api/. .
WORKDIR /app/gymlad-api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app
COPY --from=build /app/gymlad-api/out ./
ENTRYPOINT ["dotnet", "gymlad-api.dll"]