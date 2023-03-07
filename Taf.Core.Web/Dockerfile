FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Taf.Core.Web/Taf.Core.Web.csproj", "Taf.Core.Web/"]
RUN dotnet restore "Taf.Core.Web/Taf.Core.Web.csproj"
COPY . .
WORKDIR "/src/Taf.Core.Web"
RUN dotnet build "Taf.Core.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taf.Core.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taf.Core.Web.dll"]
