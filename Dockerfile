FROM node:12.7-alpine AS ng-build
WORKDIR /usr/src/app
COPY ./todo-front/package.json ./
RUN npm install
COPY ./todo-front .
RUN npx ng build --prod


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./TodoDataAPI/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./TodoDataAPI ./

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY TodoDataAPI/https /https
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=ng-build /usr/src/app/dist/todo-front ../todo-front/dist
ENTRYPOINT dotnet TodoDataAPI.dll