#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DataLawyer.API/DataLawyer.API.csproj", "DataLawyer.API/"]
COPY ["./DataLawyer.Application/DataLawyer.Application.csproj", "DataLawyer.Application/"]
COPY ["./DataLawyer.CrossCutting/DataLawyer.CrossCutting.csproj", "DataLawyer.CrossCutting/"]
COPY ["./DataLawyer.Domain/DataLawyer.Domain.csproj", "DataLawyer.Domain/"]
COPY ["./DataLawyer.Infrastructure/DataLawyer.Infrastructure.csproj", "DataLawyer.Infrastructure/"]
RUN dotnet restore "./DataLawyer.API/DataLawyer.API.csproj"
COPY . .
WORKDIR "/src/DataLawyer.API"
RUN dotnet build "./DataLawyer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DataLawyer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataLawyer.API.dll"]