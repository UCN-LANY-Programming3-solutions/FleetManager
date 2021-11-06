#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FleetManager.WebAPI/FleetManager.WebAPI.csproj", "FleetManager.WebAPI/"]
COPY ["FleetManager.DataAccessLayer/FleetManager.DataAccessLayer.csproj", "FleetManager.DataAccessLayer/"]
COPY ["FleetManager.Entities/FleetManager.Entities.csproj", "FleetManager.Entities/"]
COPY ["FleetManager.Database/FleetManager.Database.csproj", "FleetManager.Database/"]
RUN dotnet restore "FleetManager.WebAPI/FleetManager.WebAPI.csproj"
COPY . .
WORKDIR "/src/FleetManager.WebAPI"
RUN dotnet build "FleetManager.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetManager.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetManager.WebAPI.dll"]