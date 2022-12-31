#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8081
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY *.sln .
COPY bbxp.lib/*.csproj ./bbxp.lib/
COPY bbxp.web.api/*.csproj ./bbxp.web.api/

RUN dotnet restore "bbxp.lib/bbxp.lib.csproj"

COPY ["bbxp.web.api/bbxp.web.api.csproj", "bbxp.web.api/"]
RUN dotnet restore "bbxp.web.api/bbxp.web.api.csproj"
COPY . .
WORKDIR "/src/bbxp.web.api"
RUN dotnet build "bbxp.web.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bbxp.web.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bbxp.web.api.dll", "http://+:8081"]