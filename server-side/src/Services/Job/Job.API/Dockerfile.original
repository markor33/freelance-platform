#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/JobManagement/JobManagement.API/JobManagement.API.csproj", "src/Services/JobManagement/JobManagement.API/"]
RUN dotnet restore "src/Services/JobManagement/JobManagement.API/JobManagement.API.csproj"
COPY . .
WORKDIR "/src/src/Services/JobManagement/JobManagement.API"
RUN dotnet build "JobManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobManagement.API.dll"]