#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApiOrganizacao/WebApiOrganizacao.csproj", "WebApiOrganizacao/"]
COPY ["Organizacao.Dominio/Organizacao.Dominio.csproj", "Organizacao.Dominio/"]
COPY ["EDSCore/EDSCore.csproj", "EDSCore/"]
COPY ["OrganizacaoDTOS/OrganizacaoDTOS.csproj", "OrganizacaoDTOS/"]
COPY ["ServiceImagemOrganizacao/ServiceImagemOrganizacao.csproj", "ServiceImagemOrganizacao/"]
COPY ["ServiceOrganizacoes/ServiceOrganizacoes.csproj", "ServiceOrganizacoes/"]
COPY ["RepoGenerico/RepoGenerico.csproj", "RepoGenerico/"]
COPY ["RepoOrganizacao/RepoOrganizacao.csproj", "RepoOrganizacao/"]
COPY ["ValidacaoHelper/ValidacaoHelper.csproj", "ValidacaoHelper/"]
RUN dotnet restore "WebApiOrganizacao/WebApiOrganizacao.csproj"
COPY . .
WORKDIR "/src/WebApiOrganizacao"
RUN dotnet build "WebApiOrganizacao.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiOrganizacao.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiOrganizacao.dll"]