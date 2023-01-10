FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src
COPY ./Mastermind.sln .
COPY ./Mastermind/Mastermind.csproj ./Mastermind/
COPY ./MastermindTests/MastermindTests.csproj ./MastermindTests/
RUN ["dotnet", "restore"]

COPY . .

FROM base AS Test
ENTRYPOINT [ "dotnet", "test" ]

FROM base AS publish
RUN dotnet publish "./Mastermind/Mastermind.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0-bullseye-slim AS runtime

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Mastermind.dll"]