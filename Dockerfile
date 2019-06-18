FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY gymlad-api/*.csproj .
RUN dotnet restore

COPY gymlad-api/. .

RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
ENV ASPNETCORE_URLS http://*:$PORT
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "GymLad.dll"]