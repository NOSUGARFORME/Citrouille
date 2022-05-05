FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Citrouille.Api/Citrouille.Api.csproj", "Citrouille.Api/"]
COPY ["src/Citrouille.Infrastructure/Citrouille.Infrastructure.csproj", "Citrouille.Infrastructure/"]
COPY ["src/Citrouille.Application/Citrouille.Application.csproj", "Citrouille.Application/"]
COPY ["src/Citrouille.Data/Citrouille.Data.csproj", "Citrouille.Data/"]
COPY ["src/Shared/Citrouille.Shared.Abstractions/Citrouille.Shared.Abstractions.csproj", "Citrouille.Shared.Abstractions/"]
COPY ["src/Shared/Citrouille.Shared/Citrouille.Shared.csproj", "Citrouille.Shared/"]
RUN dotnet restore "src/Citrouille.Api/Citrouille.Api.csproj"
COPY . .
WORKDIR "/src/Citrouille.Api"
RUN dotnet build "Citrouille.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Citrouille.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Citrouille.Api.dll"]
