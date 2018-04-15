FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY Grayson.ExampleCQRS.sln ./
COPY Grayson.ExampleCQRS.Ritten.Host.ConsoleApp/Grayson.ExampleCQRS.Ritten.Host.ConsoleApp.csproj Grayson.ExampleCQRS.Ritten.Host.ConsoleApp/
COPY Grayson.ExampleCQRS.Ritten.Infrastructure/Grayson.ExampleCQRS.Ritten.Infrastructure.csproj Grayson.ExampleCQRS.Ritten.Infrastructure/
COPY Grayson.ExampleCQRS.KmStanden.Domain/Grayson.ExampleCQRS.KmStanden.Domain.csproj Grayson.ExampleCQRS.KmStanden.Domain/
COPY Grayson.SeedWork.DDD.Domain/Grayson.SeedWork.DDD.Domain.csproj Grayson.SeedWork.DDD.Domain/
COPY Grayson.Seedwork.DDD.Infrastructure/Grayson.SeedWork.DDD.Infrastructure.csproj Grayson.Seedwork.DDD.Infrastructure/
COPY Grayson.ExampleCQRS.Ritten.Domain/Grayson.ExampleCQRS.Ritten.Domain.csproj Grayson.ExampleCQRS.Ritten.Domain/
COPY Grayson.ExampleCQRS.ReadModel.Domain/Grayson.ExampleCQRS.ReadModel.Domain.csproj Grayson.ExampleCQRS.ReadModel.Domain/
COPY Grayson.ExampleCQRS.Infrastructure/Grayson.ExampleCQRS.Infrastructure.csproj Grayson.ExampleCQRS.Infrastructure/
COPY Grayson.SeedWork.DDD.Application/Grayson.SeedWork.DDD.Application.csproj Grayson.SeedWork.DDD.Application/
COPY Grayson.ExampleCQRS.Ritten.Application/Grayson.ExampleCQRS.Ritten.Application.csproj Grayson.ExampleCQRS.Ritten.Application/
COPY Grayson.Utils.DDD/Grayson.Utils.DDD.csproj Grayson.Utils.DDD/
COPY Grayson.Utils.Configuration/Grayson.Utils.Configuration.csproj Grayson.Utils.Configuration/
COPY Grayson.Utils.Logging/Grayson.Utils.Logging.csproj Grayson.Utils.Logging/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Grayson.ExampleCQRS.Ritten.Host.ConsoleApp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Grayson.ExampleCQRS.Ritten.Host.ConsoleApp.dll"]
