# Use the .NET 8 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Use the .NET 8 SDK for the build process
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore project dependencies
COPY ["ProjectB.csproj", "./"]
RUN dotnet restore "./ProjectB.csproj"

# Copy the rest of your app files and build it
COPY . .
WORKDIR "/src/."
RUN dotnet build "ProjectB.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "ProjectB.csproj" -c Release -o /app/publish

# Final stage: use the runtime to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectB.dll"]
