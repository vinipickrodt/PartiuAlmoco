#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update -yq && \ 
    apt-get install -yq npm && \
    npm install npm@latest -g
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PartiuAlmoco.Web.Client/PartiuAlmoco.Web.Client.csproj", "PartiuAlmoco.Web.Client/"]
RUN dotnet restore "PartiuAlmoco.Web.Client/PartiuAlmoco.Web.Client.csproj"
COPY . .
WORKDIR "/src/PartiuAlmoco.Web.Client"
RUN dotnet build "PartiuAlmoco.Web.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PartiuAlmoco.Web.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PartiuAlmoco.Web.Client.dll"]
