FROM microsoft/dotnet:2.2-sdk as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY Auth20_V1.csproj /build/

RUN dotnet restore ./build/Auth20_V1.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./Auth20_V1.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "Auth2.0_V1.dll"] 