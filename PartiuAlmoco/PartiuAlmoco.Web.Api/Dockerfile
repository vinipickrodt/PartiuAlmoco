#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PartiuAlmoco.Web.Api/PartiuAlmoco.Web.Api.csproj", "PartiuAlmoco.Web.Api/"]
COPY ["PartiuAlmoco.Application/PartiuAlmoco.Application.csproj", "PartiuAlmoco.Application/"]
COPY ["PartiuAlmoco.Core.Domain/PartiuAlmoco.Core.Domain.csproj", "PartiuAlmoco.Core.Domain/"]
COPY ["PartiuAlmoco.Infra.Domain/PartiuAlmoco.Infra.Domain.csproj", "PartiuAlmoco.Infra.Domain/"]
RUN dotnet restore "PartiuAlmoco.Web.Api/PartiuAlmoco.Web.Api.csproj"
COPY . .
WORKDIR "/src/PartiuAlmoco.Web.Api"
RUN dotnet build "PartiuAlmoco.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PartiuAlmoco.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PartiuAlmoco.Web.Api.dll"]