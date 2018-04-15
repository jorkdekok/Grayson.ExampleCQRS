FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY Grayson.ExampleCQRS.sln ./
COPY Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp/Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp.csproj Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp/
COPY Grayson.ExampleCQRS.KmStanden.Infrastructure/Grayson.ExampleCQRS.KmStanden.Infrastructure.csproj Grayson.ExampleCQRS.KmStanden.Infrastructure/
COPY Grayson.ExampleCQRS.KmStanden.Application/Grayson.ExampleCQRS.KmStanden.Application.csproj Grayson.ExampleCQRS.KmStanden.Application/
COPY Grayson.ExampleCQRS.KmStanden.Domain/Grayson.ExampleCQRS.KmStanden.Domain.csproj Grayson.ExampleCQRS.KmStanden.Domain/
COPY Grayson.SeedWork.DDD.Domain/Grayson.SeedWork.DDD.Domain.csproj Grayson.SeedWork.DDD.Domain/
COPY Grayson.SeedWork.DDD.Application/Grayson.SeedWork.DDD.Application.csproj Grayson.SeedWork.DDD.Application/
COPY Grayson.Seedwork.DDD.Infrastructure/Grayson.SeedWork.DDD.Infrastructure.csproj Grayson.Seedwork.DDD.Infrastructure/
COPY Grayson.ExampleCQRS.Ritten.Domain/Grayson.ExampleCQRS.Ritten.Domain.csproj Grayson.ExampleCQRS.Ritten.Domain/
COPY Grayson.ExampleCQRS.ReadModel.Domain/Grayson.ExampleCQRS.ReadModel.Domain.csproj Grayson.ExampleCQRS.ReadModel.Domain/
COPY Grayson.ExampleCQRS.Infrastructure/Grayson.ExampleCQRS.Infrastructure.csproj Grayson.ExampleCQRS.Infrastructure/
COPY Grayson.Utils.DDD/Grayson.Utils.DDD.csproj Grayson.Utils.DDD/
COPY Grayson.Utils.Configuration/Grayson.Utils.Configuration.csproj Grayson.Utils.Configuration/
COPY Grayson.Utils.Logging/Grayson.Utils.Logging.csproj Grayson.Utils.Logging/
COPY Grayson.ExampleCQRS.ReadModel.Infrastructure/Grayson.ExampleCQRS.ReadModel.Infrastructure.csproj Grayson.ExampleCQRS.ReadModel.Infrastructure/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Grayson.ExampleCQRS.KmStanden.Host.ConsoleApp.dll"]
