#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/PhloSales.Server/PhloSales.Server.csproj", "src/PhloSales.Server/"]
COPY ["src/PhloSales.Core/PhloSales.Core.csproj", "src/PhloSales.Core/"]
COPY ["src/PhloSales.Data/PhloSales.Data.csproj", "src/PhloSales.Data/"]
RUN dotnet restore "src/PhloSales.Server/PhloSales.Server.csproj"
COPY . .
WORKDIR "/src/src/PhloSales.Server"
RUN dotnet build "PhloSales.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhloSales.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhloSales.Server.dll"]