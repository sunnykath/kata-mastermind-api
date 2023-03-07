FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src
COPY ./Mastermind.sln .
COPY ./Mastermind.Application/Mastermind.Application.csproj ./Mastermind.Application/
COPY ./Mastermind.API/Mastermind.API.csproj ./Mastermind.API/
COPY ./Mastermind.Console/Mastermind.Console.csproj ./Mastermind.Console/
COPY ./Mastermind.Domain/Mastermind.Domain.csproj ./Mastermind.Domain/
COPY ./Mastermind.Infrastructure/Mastermind.Infrastructure.csproj ./Mastermind.Infrastructure/
COPY ./MastermindTests/MastermindTests.csproj ./MastermindTests/
RUN ["dotnet", "restore"]

COPY . .

FROM base AS test
ENTRYPOINT [ "dotnet", "test" ]

FROM base AS publish
RUN dotnet publish "./Mastermind.API/Mastermind.API.csproj" -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS runtime

WORKDIR /app

EXPOSE 443
EXPOSE 80

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Mastermind.API.dll"]