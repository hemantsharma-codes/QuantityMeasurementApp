# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files
COPY ModelLayer/*.csproj ModelLayer/
COPY RepoLayer/*.csproj RepoLayer/
COPY BusinessLayer/*.csproj BusinessLayer/
COPY ControllerLayer/*.csproj ControllerLayer/

# Restore
RUN dotnet restore ControllerLayer/ControllerLayer.csproj

# Copy everything
COPY . .

# Publish
WORKDIR /src/ControllerLayer
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "ControllerLayer.dll"]