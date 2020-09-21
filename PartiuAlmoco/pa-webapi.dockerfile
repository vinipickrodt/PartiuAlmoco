FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore PartiuAlmoco.Web.Api

# Copy everything else and build
RUN dotnet publish PartiuAlmoco.Web.Api -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
CMD dotnet "PartiuAlmoco.Web.Api.dll" 