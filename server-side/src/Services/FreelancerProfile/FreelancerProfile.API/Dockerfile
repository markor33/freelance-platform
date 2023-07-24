#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/FreelancerProfile/FreelancerProfile.API/FreelancerProfile.API.csproj", "src/Services/FreelancerProfile/FreelancerProfile.API/"]
RUN dotnet restore "src/Services/FreelancerProfile/FreelancerProfile.API/FreelancerProfile.API.csproj"
COPY . .
WORKDIR "/src/src/Services/FreelancerProfile/FreelancerProfile.API"
RUN dotnet build "FreelancerProfile.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreelancerProfile.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreelancerProfile.API.dll"]
