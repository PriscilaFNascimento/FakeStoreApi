#!/bin/bash

# Wait for the database to be ready
echo "Waiting for database to be ready..."
while ! nc -z db 5432; do
  sleep 0.1
done
echo "Database is ready!"

# Run migrations
echo "Running database migrations..."
cd /src
dotnet ef database update --project Persistence/Persistence.csproj --startup-project FakeStoreApi/FakeStoreApi.csproj

# Start the application
echo "Starting the application..."
cd /app
dotnet FakeStoreApi.dll 