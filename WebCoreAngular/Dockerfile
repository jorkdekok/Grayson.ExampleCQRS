FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 3766
EXPOSE 44334

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY WebCoreAngular/WebCoreAngular.csproj WebCoreAngular/
RUN dotnet restore WebCoreAngular/WebCoreAngular.csproj
COPY . .
WORKDIR /src/WebCoreAngular
RUN dotnet build WebCoreAngular.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebCoreAngular.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebCoreAngular.dll"]
