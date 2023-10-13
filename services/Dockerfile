FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Authentication.Service/Authentication.Service.csproj Authentication.Service/
COPY Application.Dtos/Application.Dtos.csproj Application.Dtos/
COPY Library/Application.Base.Models/Application.Base.Models.csproj Application.Base.Models/
COPY Library/Application.Business.Rules/Application.Business.Rules.csproj Application.Business.Rules/
COPY Library/Application.Core/Application.Core.csproj Application.Core/
COPY Library/Application.Database/Application.Database.csproj Application.Database/
COPY Library/Application.Exceptions/Application.Exceptions.csproj Application.Exceptions/
COPY Library/Application.Extensions/Application.Extensions.csproj Application.Extensions/
COPY Library/Application.Interfaces/Application.Interfaces.csproj Application.Interfaces/
COPY Library/Application.Messages/Application.Messages.csproj Application.Messages/
COPY Library/Application.Middleware/Application.Middleware.csproj Application.Middleware/
COPY Library/Application.Repositories/Application.Repositories.csproj Application.Repositories/
COPY Library/Application.Repositories.Services/Application.Repositories.Services.csproj Application.Repositories.Services/
COPY Library/Application.Security/Application.Security.csproj Application.Security/
COPY Library/Application.Services/Application.Services.csproj Application.Services/
COPY Library/Application.Utils/Application.Utils.csproj Application.Utils/
RUN dotnet restore "Authentication.Service/Authentication.Service.csproj"
COPY . .
WORKDIR "/src/Authentication.Service"
RUN dotnet build "Authentication.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Service.dll"]