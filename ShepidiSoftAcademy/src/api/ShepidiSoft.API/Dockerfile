# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Projeyi kopyala
COPY . .

# Restore + publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

# Render port ayarı
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# API DLL adı (BUNU KENDİ PROJENE GÖRE KONTROL ET)
ENTRYPOINT ["dotnet", "ShepidiSoft.API.dll"]
