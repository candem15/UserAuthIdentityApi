FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["UserAuthIdentityApi.csproj", "./"]
RUN dotnet restore "UserAuthIdentityApi.csproj"
COPY . .
RUN dotnet publish "UserAuthIdentityApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserAuthIdentityApi.dll"]