#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/FeedbackManagement/FeedbackManagement.API/FeedbackManagement.API.csproj", "src/Services/FeedbackManagement/FeedbackManagement.API/"]
RUN dotnet restore "src/Services/FeedbackManagement/FeedbackManagement.API/FeedbackManagement.API.csproj"
COPY . .
WORKDIR "/src/src/Services/FeedbackManagement/FeedbackManagement.API"
RUN dotnet build "FeedbackManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeedbackManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeedbackManagement.API.dll"]