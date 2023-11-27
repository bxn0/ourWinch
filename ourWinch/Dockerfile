# --- [Base Stage] ---
# <summary>
# The base stage uses the ASP.NET Core runtime image to create the environment for running the application.
# It exposes ports 80 and 443 for HTTP and HTTPS traffic.
# </summary>
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app # Set the working directory in the container to /app
EXPOSE 80 
EXPOSE 443 

# --- [Build Stage] ---
# <summary>
# The build stage uses the .NET SDK image to compile the application.
# It copies the project file and restores NuGet packages, then copies the source code and builds the release binary.
# </summary>
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src # Set the working directory in the container to /src
COPY ["ourWinch.csproj", "./"] # Copy the CSPROJ file and restore any dependencies (via NuGet)
RUN dotnet restore "ourWinch.csproj"
COPY . . 
RUN dotnet build "ourWinch.csproj" -c Release -o /app/build # Build the application in release mode and output to the build directory

# --- [Publish Stage] ---
# <summary>
# The publish stage builds upon the build stage and compiles the application into a final deployable unit.
# It uses the `dotnet publish` command to prepare the application for deployment.
# </summary>
FROM build AS publish
RUN dotnet publish "ourWinch.csproj" -c Release -o /app/publish /p:UseAppHost=false 

# --- [Final Stage] ---
# <summary>
# The final stage prepares the deployable Docker image.
# It copies the published application from the publish stage into the runtime environment.
# The resulting image is lean and contains only what is necessary to run the application.
# </summary>
FROM base AS final
WORKDIR /app # Set the working directory to /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "ourWinch.dll"] # Set the Docker container to run the application when it starts




# docker build -t umity/ourwinch:tag .
# docker login
# docker push umity/ourwinch:tag

# På terminal må skrives
# docker pull umity/ourwinch:tag
# docker run -d -p 8080:5002 umity/ourwinch:tag
# docker ps
