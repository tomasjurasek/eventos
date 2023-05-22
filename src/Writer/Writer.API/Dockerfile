#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Writer.API/Writer.API.csproj", "Writer.API/"]
RUN dotnet restore "Writer.API/Writer.API.csproj"
COPY . .
WORKDIR "/src/Writer.API"
RUN dotnet build "Writer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Writer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Writer.API.dll"]