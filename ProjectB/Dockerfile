# Use the official .NET image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProjectB.csproj", "./"]
RUN dotnet restore "./ProjectB.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ProjectB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectB.dll"]
