#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine3.19 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.19 AS build
WORKDIR "/src"

COPY *.sln .
COPY bbxp.lib/*.csproj ./bbxp.lib/
COPY bbxp.web.mvc/*.csproj ./bbxp.web.mvc/

RUN dotnet restore "bbxp.lib/bbxp.lib.csproj"

RUN dotnet restore "bbxp.web.mvc/bbxp.web.mvc.csproj"
COPY . .
WORKDIR "/src/bbxp.web.mvc"
RUN dotnet build "bbxp.web.mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bbxp.web.mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bbxp.web.mvc.dll", "http://+:8080"]