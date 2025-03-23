# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files
COPY ["FakeStoreApi/FakeStoreApi.csproj", "FakeStoreApi/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Tests/Tests.csproj", "Tests/"]

# Restore packages
RUN dotnet restore "FakeStoreApi/FakeStoreApi.csproj"

# Copy all source code
COPY . .

# Build the project
WORKDIR "/src/FakeStoreApi"
RUN dotnet build "FakeStoreApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FakeStoreApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef

# Copy published files
COPY --from=publish /app/publish .

# Copy project files for migrations
COPY --from=build /src/Domain/Domain.csproj /src/Domain/
COPY --from=build /src/Persistence/Persistence.csproj /src/Persistence/
COPY --from=build /src/FakeStoreApi/FakeStoreApi.csproj /src/FakeStoreApi/
COPY --from=build /src/Tests/Tests.csproj /src/Tests/

# Copy source code for migrations
COPY --from=build /src/Domain /src/Domain
COPY --from=build /src/Persistence /src/Persistence
COPY --from=build /src/FakeStoreApi /src/FakeStoreApi
COPY --from=build /src/Tests /src/Tests

# Copy startup script
COPY FakeStoreApi/startup.sh .
RUN chmod +x startup.sh

# Install netcat for database connection check
RUN apt-get update && apt-get install -y netcat-openbsd && rm -rf /var/lib/apt/lists/*

# Add dotnet tools to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["./startup.sh"]