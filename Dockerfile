FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src
COPY ./Mastermind.sln .
COPY ./Mastermind/Mastermind.csproj ./Mastermind/
COPY ./MastermindTests/MastermindTests.csproj ./MastermindTests/
RUN ["dotnet", "restore"]

COPY . .

FROM base AS Test
ENTRYPOINT [ "dotnet", "test" ]

