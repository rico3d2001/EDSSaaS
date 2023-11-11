#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["APIContratos/APIContratos.csproj", "APIContratos/"]
COPY ["Contratos.Dominio/Contratos.Dominio.csproj", "Contratos.Dominio/"]
COPY ["ContratoDTOs/ContratoDTOs.csproj", "ContratoDTOs/"]
COPY ["EDSCore/EDSCore.csproj", "EDSCore/"]
COPY ["ServiceContratos/ServiceContratos.csproj", "ServiceContratos/"]
COPY ["RepositoriosContratos/RepositoriosContratos.csproj", "RepositoriosContratos/"]
COPY ["ValidacaoHelper/ValidacaoHelper.csproj", "ValidacaoHelper/"]
RUN dotnet restore "APIContratos/APIContratos.csproj"
COPY . .
WORKDIR "/src/APIContratos"
RUN dotnet build "APIContratos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIContratos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIContratos.dll"]