FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WarrinaAspCore/WarrinaAspCore.csproj", "WarrinaAspCore/"]
RUN dotnet restore "WarrinaAspCore/WarrinaAspCore.csproj"
COPY . .
WORKDIR "/src/WarrinaAspCore"
RUN dotnet build "WarrinaAspCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WarrinaAspCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarrinaAspCore.dll"]