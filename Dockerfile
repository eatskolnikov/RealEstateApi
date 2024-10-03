FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /app

COPY . ./

RUN dotnet restore RealEstateListingApi.csproj
RUN dotnet publish RealEstateListingApi.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

EXPOSE 8080

ENTRYPOINT [ "dotnet", "RealEstateListingApi.dll"]