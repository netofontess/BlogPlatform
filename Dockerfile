FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["BlogPlatform.Api/BlogPlatform.Api.csproj", "BlogPlatform.Api/"]
RUN dotnet restore "BlogPlatform.Api/BlogPlatform.Api.csproj"
COPY . .
WORKDIR "/src/BlogPlatform.Api"
RUN dotnet build "BlogPlatform.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogPlatform.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogPlatform.Api.dll"]
