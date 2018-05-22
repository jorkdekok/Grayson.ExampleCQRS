FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Grayson.ExampleCQRS.ReadModel.WepApi/Grayson.ExampleCQRS.ReadModel.WepApi.csproj Grayson.ExampleCQRS.ReadModel.WepApi/
COPY Grayson.ExampleCQRS.ReadModel.Domain/Grayson.ExampleCQRS.ReadModel.Domain.csproj Grayson.ExampleCQRS.ReadModel.Domain/
COPY Grayson.Utils.Configuration/Grayson.Utils.Configuration.csproj Grayson.Utils.Configuration/
COPY Grayson.ExampleCQRS.ReadModel.Infrastructure/Grayson.ExampleCQRS.ReadModel.Infrastructure.csproj Grayson.ExampleCQRS.ReadModel.Infrastructure/
COPY Grayson.ExampleCQRS.Infrastructure/Grayson.ExampleCQRS.Infrastructure.csproj Grayson.ExampleCQRS.Infrastructure/
COPY Grayson.Seedwork.DDD.Infrastructure/Grayson.SeedWork.DDD.Infrastructure.csproj Grayson.Seedwork.DDD.Infrastructure/
COPY Grayson.SeedWork.DDD.Domain/Grayson.SeedWork.DDD.Domain.csproj Grayson.SeedWork.DDD.Domain/
COPY Grayson.SeedWork.DDD.Application/Grayson.SeedWork.DDD.Application.csproj Grayson.SeedWork.DDD.Application/
RUN dotnet restore Grayson.ExampleCQRS.ReadModel.WepApi/Grayson.ExampleCQRS.ReadModel.WepApi.csproj
COPY . .
WORKDIR /src/Grayson.ExampleCQRS.ReadModel.WepApi
RUN dotnet build Grayson.ExampleCQRS.ReadModel.WepApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Grayson.ExampleCQRS.ReadModel.WepApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Grayson.ExampleCQRS.ReadModel.WepApi.dll"]
