#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY CustomerService/*.csproj ./CustomerService/
COPY ServiceCommon/*.csproj ./ServiceCommon/
COPY CustomerService/NuGet.Config ./CustomerService/
COPY CustomerService/NuGet.Config ./ServiceCommon/

RUN dotnet restore ./CustomerService/CustomerService.csproj

COPY CustomerService/. ./CustomerService/
COPY ServiceCommon/. ./ServiceCommon/

WORKDIR /src/CustomerService
RUN dotnet build "CustomerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.dll"]
