FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src
COPY ./Mastermind.sln .
COPY ./Mastermind.Application/Mastermind.Application.csproj ./Mastermind.Application/
COPY ./Mastermind.Presentation/Mastermind.Presentation.csproj ./Mastermind.Presentation/
COPY ./Mastermind.Domain/Mastermind.Domain.csproj ./Mastermind.Domain/
COPY ./MastermindTests/MastermindTests.csproj ./MastermindTests/
RUN ["dotnet", "restore"]

COPY . .

FROM base AS Test
ENTRYPOINT [ "dotnet", "test" ]

