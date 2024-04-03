#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Overflower.Api/Overflower.Api.csproj", "Overflower.Api/"]
COPY ["Overflower.Application/Overflower.Application.csproj", "Overflower.Application/"]
COPY ["Overflower.Persistence/Overflower.Persistence.csproj", "Overflower.Persistence/"]
COPY ["Overflower.Infrastructure/Overflower.Infrastructure.csproj", "Overflower.Infrastructure/"]
RUN dotnet restore "Overflower.Api/Overflower.Api.csproj"
COPY . .
WORKDIR "/src/Overflower.Api"
RUN dotnet build "Overflower.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Overflower.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT ${ENVIRONMENT}
ENTRYPOINT ["dotnet", "Overflower.Api.dll"]