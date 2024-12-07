# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

EXPOSE 8080
EXPOSE 8081

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore "ChatApp.API/ChatApp.API.csproj"

# Build the API
RUN dotnet build "ChatApp.API/ChatApp.API.csproj" -c Release -o /app/build

# Publish the build
RUN dotnet publish "ChatApp.API/ChatApp.API.csproj" -c Release -o /app/publish

# Use the official runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ChatApp.API.dll"]
