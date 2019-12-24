FROM mcr.microsoft.com/dotnet/core/sdk:2.2
 
WORKDIR /home/app
 
COPY ./Auth20_V1/Auth20_V1.csproj ./Auth20_V1/
COPY ./Auth20_V1.sln .
 
RUN dotnet restore
 
COPY . .
 
RUN dotnet publish ./Auth20_V1/Auth20_V1.csproj -o /publish/
 
WORKDIR /publish
 
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
 
ENTRYPOINT ["dotnet", "Auth20_V1.dll"]
