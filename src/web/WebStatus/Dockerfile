ARG NET_IMAGE=7.0-bullseye-slim
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
# Create this "restore-solution" section by running ./Create-DockerfileSolutionRestore.ps1, to optimize build cache reuse
COPY ["src/libraries/general/libraries.csproj", "src/libraries/general/"]
COPY ["src/libraries/fields/fields.csproj", "src/libraries/fields/"]
COPY ["src/services/Bouvet.CV.Service/Bouvet.CV.Service.csproj", "src/services/Bouvet.CV.Service/"]
COPY ["src/services/Bouvet.Notification.Service/Bouvet.Notification.Service.csproj", "src/services/Bouvet.Notification.Service/"]
COPY ["src/services/Bouvet.Rule.Engine.Service/Bouvet.Rule.Engine.Service.csproj", "src/services/Bouvet.Rule.Engine.Service/"]
COPY ["src/services/CV.Partner.Service/CV.Partner.Service.csproj", "src/services/CV.Partner.Service/"]
COPY ["src/web/WebStatus/WebStatus.csproj", "src/web/WebStatus/"]
COPY ["docker-compose.dcproj", "./"]
#COPY ["NuGet.config", "./"]
COPY ["RuleEngine.sln", "./"]
#RUN dotnet restore "RuleEngine.sln"
RUN dotnet restore "src/libraries/general/libraries.csproj"
RUN dotnet restore "src/web/WebStatus/WebStatus.csproj"
COPY . .
WORKDIR "/src/src/web/WebStatus"
# RUN dotnet build "WebStatus.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish --no-restore "WebStatus.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStatus.dll"]
