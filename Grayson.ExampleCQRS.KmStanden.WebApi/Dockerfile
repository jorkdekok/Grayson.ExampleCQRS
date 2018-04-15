FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Grayson.ExampleCQRS.sln ./
COPY Grayson.ExampleCQRS.KmStanden.WebApi/Grayson.ExampleCQRS.KmStanden.WebApi.csproj Grayson.ExampleCQRS.KmStanden.WebApi/
COPY Grayson.ExampleCQRS.KmStanden.Application/Grayson.ExampleCQRS.KmStanden.Application.csproj Grayson.ExampleCQRS.KmStanden.Application/
COPY Grayson.ExampleCQRS.KmStanden.Domain/Grayson.ExampleCQRS.KmStanden.Domain.csproj Grayson.ExampleCQRS.KmStanden.Domain/
COPY Grayson.SeedWork.DDD.Domain/Grayson.SeedWork.DDD.Domain.csproj Grayson.SeedWork.DDD.Domain/
COPY Grayson.SeedWork.DDD.Application/Grayson.SeedWork.DDD.Application.csproj Grayson.SeedWork.DDD.Application/
COPY Grayson.ExampleCQRS.Infrastructure/Grayson.ExampleCQRS.Infrastructure.csproj Grayson.ExampleCQRS.Infrastructure/
COPY Grayson.Seedwork.DDD.Infrastructure/Grayson.SeedWork.DDD.Infrastructure.csproj Grayson.Seedwork.DDD.Infrastructure/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Grayson.ExampleCQRS.KmStanden.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Grayson.ExampleCQRS.KmStanden.WebApi.dll"]
