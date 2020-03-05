#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/workers/Message.Worker/Message.Worker.csproj", "src/workers/Message.Worker/"]
RUN dotnet restore "src/workers/Message.Worker/Message.Worker.csproj"
COPY . .
WORKDIR "/src/src/workers/Message.Worker"
RUN dotnet build "Message.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Message.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Message.Worker.dll"]