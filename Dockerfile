FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["AreYouOk.csproj", "./"]
RUN dotnet restore "./AreYouOk.csproj"

COPY . .
RUN dotnet build "AreYouOk.csproj" -c Release -o /app

FROM build as publish
RUN dotnet publish "AreYouOk.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base
WORKDIR /app
EXPOSE 80

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AreYouOk.dll"]