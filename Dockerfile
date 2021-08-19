FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY ./UpsMo.Common ./UpsMo.Common
COPY ./UpsMo.Data ./UpsMo.Data
COPY ./UpsMo.Entities ./UpsMo.Entities
COPY ./UpsMo.Services ./UpsMo.Services
COPY ./UpsMo.WebAPI ./UpsMo.WebAPI

WORKDIR /source/UpsMo.WebAPI
RUN dotnet publish --no-restore -c Release -o /app --no-cache /restore


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "UpsMo.WebAPI.dll"]