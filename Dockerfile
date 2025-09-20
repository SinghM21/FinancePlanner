# syntax=docker/dockerfile:1

# Build stage
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Install EF Core CLI
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy solution and project files
COPY FinancePlanner.sln .
COPY FinancePlanner/FinancePlanner.csproj FinancePlanner/
COPY FinancePlannerTests/FinancePlannerTests.csproj FinancePlannerTests/

# Restore dependencies
RUN dotnet restore FinancePlanner.sln

# Copy everything else and build
COPY . .
RUN dotnet publish FinancePlanner/FinancePlanner.csproj \
    -c Release -o /app --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app .

# Run as non-root user from base image
USER $APP_UID

ENTRYPOINT ["dotnet", "FinancePlanner.dll"]