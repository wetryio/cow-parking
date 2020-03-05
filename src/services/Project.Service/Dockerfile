#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/Entity.Service/Entity.Service.csproj", "src/services/Entity.Service/"]
RUN dotnet restore "src/services/Entity.Service/Entity.Service.csproj"
COPY . .
WORKDIR "/src/src/services/Entity.Service"
RUN dotnet build "Entity.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Entity.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Entity.Service.dll"]
