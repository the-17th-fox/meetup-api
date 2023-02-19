FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
FROM build AS publish

######## Publishing Core
COPY ["Core/", "./Core/"]
RUN dotnet publish "Core/Core.csproj" -c Release -o /app/publish /p:UseAppHost=false
######## 

######## Publishing Infrastructure
COPY ["Infrastructure/", "./Infrastructure/"]
RUN dotnet publish "Infrastructure/Infrastructure.csproj" -c Release -o /app/publish /p:UseAppHost=false
######## 

######## Publishing AccountMsvc
COPY ["Web/", "./Web/"]
RUN dotnet publish "Web/Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]