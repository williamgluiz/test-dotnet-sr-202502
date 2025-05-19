# ===== Stage base: runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ===== Stage build: restore & compile =====
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release

# 1) Entramos em /src e copiamos só o .csproj
WORKDIR /src
COPY ["ApplicantTracking.Api.csproj", "./"]

# 2) Restauramos dependências
RUN dotnet restore "ApplicantTracking.Api.csproj"

# 3) Copiamos **tudo** do contexto (que já é src/ApplicantTracking.Api)
COPY . .

# 4) Compilamos apontando para o .csproj na raiz do contexto
RUN dotnet build "ApplicantTracking.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

# ===== Stage publish: empacotar artefatos =====
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "ApplicantTracking.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# ===== Stage final: runtime com artefatos publicados =====
FROM base AS final
WORKDIR /app

# Copia o resultado publicado para a imagem final
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ApplicantTracking.Api.dll"]
