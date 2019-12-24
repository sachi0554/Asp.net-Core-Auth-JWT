FROM microsoft/aspnetcore:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.2 AS build
WORKDIR /src
COPY Auth20_V1.sln ./
COPY Auth20_V1/*.csproj ./WebAPIProject/

RUN dotnet restore
COPY . .

WORKDIR /src/WebAPIProject
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Auth2.0_V1.dll"]
