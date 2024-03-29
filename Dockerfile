FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
    WORKDIR /code
    COPY ./*.csproj .
    RUN dotnet restore
    COPY . .
    RUN dotnet build "ejercicio_xml_csharp.csproj" -c Release -o build
    RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
    WORKDIR /code
    COPY --from=build /code/out .
    ENV ASPNETCORE_ENVIRONMENT=Development
    ENTRYPOINT ["dotnet", "ejercicio_xml_csharp.dll"]