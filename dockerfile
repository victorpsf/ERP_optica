FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
RUN mkdir /app
RUN mkdir /src
EXPOSE 80

COPY ./Application.Core /src/Application.Core/
COPY ./Authentication.Service /src/Authentication.Service/
COPY ./Authentication.Service.sln /src/
COPY ./Authentication.Controller /src/Authentication.Controller/
COPY ./Authentication.Controller.Models /src/Authentication.Controller.Models/
COPY ./Application.Library /src/Application.Library/
COPY ./Shared.Services /src/Shared.Services/
COPY ./Application.Database /src/Application.Database/
COPY ./Authentication.Repositories /src/Authentication.Repositories/
COPY ./Shared.Extensions /src/Shared.Extensions/
#COPY ./Application.Messages /src/Application.Messages/

WORKDIR /src
RUN dotnet restore ./Authentication.Service.sln
WORKDIR /src/Authentication.Service/
RUN dotnet build -c Release -o /app
RUN dotnet publish -c Release -o /app

WORKDIR /app
ENTRYPOINT ["dotnet", "Authentication.Service.dll"]
