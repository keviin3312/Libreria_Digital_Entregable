FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KtorresLibreriaDigital.csproj", "./"]
RUN dotnet restore "KtorresLibreriaDigital.csproj"
COPY . .
RUN dotnet publish "KtorresLibreriaDigital.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "KtorresLibreriaDigital.dll"]
