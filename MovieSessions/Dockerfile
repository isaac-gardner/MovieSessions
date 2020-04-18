FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["MovieSessions/MovieSessions.csproj", "MovieSessions/"]
RUN dotnet restore "MovieSessions/MovieSessions.csproj"
COPY . .
WORKDIR "/src/MovieSessions"
RUN dotnet build "MovieSessions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieSessions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieSessions.dll"]