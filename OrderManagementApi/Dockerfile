FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OrderManagementApi/OrderManagementApi.csproj", "OrderManagementApi/"]
COPY ["OrderManagementApi.BusinessLogic/OrderManagementApi.BusinessLogic.csproj", "OrderManagementApi.BusinessLogic/"]
COPY ["OrderManagementApi.Infrastructure/OrderManagementApi.Infrastructure.csproj", "OrderManagementApi.Infrastructure/"]
RUN dotnet restore "OrderManagementApi/OrderManagementApi.csproj"
COPY . .
WORKDIR "/src/OrderManagementApi"
RUN dotnet build "OrderManagementApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagementApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagementApi.dll"]