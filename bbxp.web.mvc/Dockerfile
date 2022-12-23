#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
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

ENV ASPNETCORE_URLS http://*:8080

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bbxp.web.mvc.dll"]