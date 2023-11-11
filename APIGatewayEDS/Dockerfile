#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["APIGatewayEDS/APIGatewayEDS.csproj", "APIGatewayEDS/"]
COPY ["ContratoDTOs/ContratoDTOs.csproj", "ContratoDTOs/"]
COPY ["HubDTOs/HubDTOs.csproj", "HubDTOs/"]
COPY ["OrganizacaoDTOS/OrganizacaoDTOS.csproj", "OrganizacaoDTOS/"]
COPY ["ServiceFotoUsuario/ServiceFotoUsuario.csproj", "ServiceFotoUsuario/"]
COPY ["ServicoAutorizacao/ServicoAutorizacao.csproj", "ServicoAutorizacao/"]
COPY ["EDSCore/EDSCore.csproj", "EDSCore/"]
COPY ["Hubs.Dominio/Hubs.Dominio.csproj", "Hubs.Dominio/"]
COPY ["Repositorios/RepoHubs.csproj", "Repositorios/"]
COPY ["ServicosEmailHub/ServicosEmailHub.csproj", "ServicosEmailHub/"]
COPY ["ValidacaoHelper/ValidacaoHelper.csproj", "ValidacaoHelper/"]
RUN dotnet restore "APIGatewayEDS/APIGatewayEDS.csproj"
COPY . .
WORKDIR "/src/APIGatewayEDS"
RUN dotnet build "APIGatewayEDS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGatewayEDS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGatewayEDS.dll"]