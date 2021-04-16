FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["Hsec.Application/Hsec.Application.csproj", "./Hsec.Application/"]
COPY ["Hsec.Domain/Hsec.Domain.csproj", "./Hsec.Domain/"]
COPY ["Hsec.Infrastructure/Hsec.Infrastructure.csproj", "./Hsec.Infrastructure/"]
COPY ["Hsec.WebApi/Hsec.WebApi.csproj", "./Hsec.WebApi/"]
RUN dotnet restore "./Hsec.WebApi/Hsec.WebApi.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "Hsec.WebApi/Hsec.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hsec.WebApi/Hsec.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# The below is commented and the line after is used when in heroku
# ENTRYPOINT ["dotnet", "dotnetcore31heroku.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Hsec.WebApi.dll