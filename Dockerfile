# Get .NET Core v3.1 build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Change directory
WORKDIR /source

# Copy files
COPY . .

# Build & publish
RUN dotnet publish -c release -o /executable

# Get the ASP.NET v3.1 image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# Change directory
WORKDIR /app

# Copy the files from the build image into the runtime image
COPY --from=build /executable .

# Execute the project build
ENTRYPOINT ["dotnet", "BikeDataProject.API.dll"]