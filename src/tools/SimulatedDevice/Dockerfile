FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "SimulatedDevice.dll"]