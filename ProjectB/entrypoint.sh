#!/bin/bash

set -e

echo "Checking for migrations..."

# Generate a new migration if there are pending changes
dotnet ef migrations add AutoMigration --no-build --force || true

echo "Applying migrations..."
dotnet ef database update --no-build

echo "Starting the application..."
exec dotnet ProjectB.dll
