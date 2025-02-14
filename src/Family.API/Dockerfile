# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Family.API/Family.API.csproj", "src/Family.API/"]
COPY ["src/Family.Application/Family.Application.csproj", "src/Family.Application/"]
COPY ["src/Family.Domain/Family.Domain.csproj", "src/Family.Domain/"]
COPY ["src/Family.Infastructure/EntityFramework/Family.Infrastructure.EntityFramework/Family.Infrastructure.EntityFramework.csproj", "src/Family.Infastructure/EntityFramework/Family.Infrastructure.EntityFramework/"]
COPY ["src/Family.Infastructure/Repositories/Family.Infrastructure.Repositories.Implementations/Family.Infrastructure.Repositories.Implementations.csproj", "src/Family.Infastructure/Repositories/Family.Infrastructure.Repositories.Implementations/"]
RUN dotnet restore "./src/Family.API/Family.API.csproj"
COPY . .
WORKDIR "/src/src/Family.API"
RUN dotnet build "./Family.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Family.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Family.API.dll"]