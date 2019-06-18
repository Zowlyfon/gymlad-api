FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY gymlad-api/*.csproj .
RUN dotnet restore

COPY gymlad-api/. .

RUN dotnet ef database update
RUN dotnet publish -c Debug -o out
RUN rm out/GymLad.db
RUN cp GymLad.db out/

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "GymLad.dll"]