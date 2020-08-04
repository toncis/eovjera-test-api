FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57875
EXPOSE 44353

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
COPY ["eovjera.test.webapi/eovjera.test.webapi.csproj", "eOvjera.Test.WebAPI/"]
RUN dotnet restore "eovjera.test.webapi/eovjera.test.webapi.csproj"
COPY . .
WORKDIR "/src/eovjera.test.webapi"
RUN dotnet build "eovjera.test.webapi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "eovjera.test.webapi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eovjera.test.webapi.dll"]