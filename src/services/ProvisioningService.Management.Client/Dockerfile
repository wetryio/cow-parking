FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY . .
EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "ProvisioningService.Management.Client.dll"]