# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ourWinch.csproj .
RUN dotnet restore "./ourWinch.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ourWinch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ourWinch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ourWinch.dll"]
