FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

EXPOSE 8080

COPY . .

RUN dotnet restore "ChatApp.API/ChatApp.API.csproj"

RUN dotnet build "ChatApp.API/ChatApp.API.csproj" -c Release -o /app/build

RUN dotnet publish "ChatApp.API/ChatApp.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ChatApp.API.dll"]
