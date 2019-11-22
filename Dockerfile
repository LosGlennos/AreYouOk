FROM node:10-alpine as build-node
WORKDIR /ClientApp
COPY AreYouOk/ClientApp/package.json .
COPY AreYouOk/ClientApp/package-lock.json .
RUN npm install
COPY AreYouOk/ClientApp/ . 
RUN npm run build 

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
ENV BuildingDocker true
COPY ["AreYouOk/AreYouOk.csproj", "AreYouOk/"]
RUN dotnet restore "AreYouOk/AreYouOk.csproj"
COPY . .

WORKDIR "/src/AreYouOk"
RUN dotnet build "AreYouOk.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "AreYouOk.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 as base
WORKDIR /app
EXPOSE 80

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-node /ClientApp/build ./ClientApp/build
ENTRYPOINT ["dotnet", "AreYouOk.dll"]