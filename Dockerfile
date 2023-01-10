FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src
COPY ./Mastermind.sln .
COPY ./Mastermind.Application/Mastermind.Application.csproj ./Mastermind.Application/
COPY ./Mastermind.Presentation/Mastermind.Presentation.csproj ./Mastermind.Presentation/
COPY ./Mastermind.Domain/Mastermind.Domain.csproj ./Mastermind.Domain/
COPY ./MastermindTests/MastermindTests.csproj ./MastermindTests/
RUN ["dotnet", "restore"]

COPY . .

FROM base AS test
ENTRYPOINT [ "dotnet", "test" ]

FROM base AS publish
RUN dotnet publish "./Mastermind/Mastermind.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0-bullseye-slim AS runtime

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Mastermind.dll"]