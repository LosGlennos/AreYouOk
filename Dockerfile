FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["AreYouOk/AreYouOk.csproj", "AreYouOk/"]
RUN dotnet restore "AreYouOk/AreYouOk.csproj"

COPY . .

WORKDIR "/src/AreYouOk"
RUN dotnet build "AreYouOk.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "AreYouOk.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base
WORKDIR /app
EXPOSE 80

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AreYouOk.dll"]