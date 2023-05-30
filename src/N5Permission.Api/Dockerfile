#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_URLS http://*:5001

ENV ASPNETCORE_ENVIRONMENT=Development  

EXPOSE 5001

WORKDIR /src
COPY ["src/N5Permission.Api/N5Permission.Api.csproj", "src/N5Permission.Api/"]
RUN dotnet restore "src/N5Permission.Api/N5Permission.Api.csproj"
COPY . .
WORKDIR "/src/src/N5Permission.Api"
RUN dotnet build "N5Permission.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5Permission.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5Permission.Api.dll"]