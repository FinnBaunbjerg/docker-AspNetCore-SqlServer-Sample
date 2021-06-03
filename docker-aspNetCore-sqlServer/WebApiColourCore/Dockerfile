#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApiColourCore/WebApiColourCore.csproj", "WebApiColourCore/"]
# Dependen library is added man
COPY ["WebApiColourData/WebApiColourData.csproj", "WebApiColourData/"]
RUN dotnet restore "WebApiColourCore/WebApiColourCore.csproj"
COPY . .
WORKDIR "/src/WebApiColourCore"
RUN dotnet build "WebApiColourCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiColourCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiColourCore.dll"]