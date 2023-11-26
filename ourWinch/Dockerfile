# Use the official ASP.NET Core runtime image from Microsoft as the base image
# This image will be used to run the final application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app # Set the working directory in the container to /app
EXPOSE 80 # Inform Docker that the container listens on port 80 (HTTP)
EXPOSE 443 # Inform Docker that the container listens on port 443 (HTTPS)

# Use the official .NET SDK image from Microsoft to build the application
# This image includes the .NET SDK and is used during the build process
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src # Set the working directory in the container to /src
COPY ["ourWinch.csproj", "./"] # Copy the CSPROJ file and restore any dependencies (via NuGet)
RUN dotnet restore "ourWinch.csproj"
COPY . . # Copy the rest of the source code
RUN dotnet build "ourWinch.csproj" -c Release -o /app/build # Build the application in release mode and output to the build directory

# Create a publish stage that will compile the application
# This stage compiles the application's code and its dependencies into a deployable unit
FROM build AS publish
RUN dotnet publish "ourWinch.csproj" -c Release -o /app/publish /p:UseAppHost=false # Publish the application's code

# Use the base image again for the final image
# This image is what will actually be deployed and contains only the runtime and the built application
FROM base AS final
WORKDIR /app # Set the working directory to /app
COPY --from=publish /app/publish . # Copy the published application from the publish stage to the container
ENTRYPOINT ["dotnet", "ourWinch.dll"] # Set the Docker container to run the application when it starts




# docker build -t umity/ourwinch:tag .
# docker login
# docker push umity/ourwinch:tag

# På terminal må skrives
# docker pull umity/ourwinch:tag
# docker run -d -p 8080:5002 umity/ourwinch:tag
# docker ps
