# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only project files for dependency caching
COPY ["CQ.AuthProvider.Abstractions/*.csproj", "CQ.AuthProvider.Abstractions/"]
COPY ["CQ.IdentityProvider.EfCore/*.csproj", "CQ.IdentityProvider.EfCore/"]
COPY ["CQ.AuthProvider.DataAccess.EfCore/*.csproj", "CQ.AuthProvider.DataAccess.EfCore/"]
COPY ["CQ.AuthProvider.BusinessLogic/*.csproj", "CQ.AuthProvider.BusinessLogic/"]
COPY ["CQ.AuthProvider.WebApi/*.csproj", "CQ.AuthProvider.WebApi/"]
WORKDIR CQ.AuthProvider.WebApi
RUN dotnet restore
RUN dotnet build "CQ.AuthProvider.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQ.AuthProvider.WebApi.dll"]