FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Tss/Tss.csproj", "Tss/"]
RUN dotnet restore "Tss/Tss.csproj"
COPY . .
WORKDIR "/src/Tss"
RUN dotnet build "Tss.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Tss.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tss.dll"]