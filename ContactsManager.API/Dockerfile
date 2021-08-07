#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ContactsManager.API/ContactsManager.API.csproj", "ContactsManager.API/"]
RUN dotnet restore "ContactsManager.API/ContactsManager.API.csproj"
COPY . .
WORKDIR "/src/ContactsManager.API"
RUN dotnet build "ContactsManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsManager.API.dll"]