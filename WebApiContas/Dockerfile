#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApiContas/WebApiContas.csproj", "WebApiContas/"]
COPY ["Conta.Dominio/Conta.Dominio.csproj", "Conta.Dominio/"]
COPY ["ContaDTOs/ContaDTOs.csproj", "ContaDTOs/"]
COPY ["EDSCore/EDSCore.csproj", "EDSCore/"]
COPY ["RepoConta/RepoConta.csproj", "RepoConta/"]
COPY ["RepoGenerico/RepoGenerico.csproj", "RepoGenerico/"]
COPY ["ServiceContas/ServiceContas.csproj", "ServiceContas/"]
RUN dotnet restore "WebApiContas/WebApiContas.csproj"
COPY . .
WORKDIR "/src/WebApiContas"
RUN dotnet build "WebApiContas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiContas.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiContas.dll"]