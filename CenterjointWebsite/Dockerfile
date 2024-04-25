#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
WORKDIR /app/AppData/UploadImages
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CenterjointWebsite/CenterjointWebsite.csproj", "CenterjointWebsite/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["OfficialWebsite.Core/OfficialWebsite.Core.csproj", "OfficialWebsite.Core/"]
RUN dotnet restore "./CenterjointWebsite/./CenterjointWebsite.csproj"
COPY . .
WORKDIR "/src/CenterjointWebsite"
RUN dotnet build "./CenterjointWebsite.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CenterjointWebsite.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CenterjointWebsite.dll"]