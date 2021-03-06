FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
RUN apt-get update -yq && \ 
    apt-get install -yq npm && \
    npm install npm@latest -g
    
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
CMD dotnet "PartiuAlmoco.Web.Client.dll" 