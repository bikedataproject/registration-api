# Get .NET Core v3.1 build image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build

# copy api assemblies and files
RUN mkdir /var/app
RUN mkdir /var/app/logs
COPY ./bin/release/netcoreapp3.1/publish /var/app
# COPY appsettings.json /var/app/appsettings.json

# couple data folder data volume.
VOLUME ["/var/app/data"]
VOLUME ["/var/app/logs"]

# SET base url
ENV ASPNETCORE_URLS http://+:5000

# Expose ports
EXPOSE 5000

# Execute the project build
WORKDIR /var/app/
ENTRYPOINT ["dotnet", "BikeDataProject.Registrations.API.dll"]
