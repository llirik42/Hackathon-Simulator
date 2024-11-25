FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /build
COPY HackathonProblem.Developer HackathonProblem.Developer
COPY HackathonProblem.Common HackathonProblem.Common
COPY HackathonProblem.CsvEmployeeProvider HackathonProblem.CsvEmployeeProvider
COPY HackathonProblem.RandomWishlistsProvider HackathonProblem.RandomWishlistsProvider
RUN dotnet restore ./HackathonProblem.Developer/HackathonProblem.Developer.csproj
RUN dotnet publish ./HackathonProblem.Developer/HackathonProblem.Developer.csproj -c Release -o result

FROM base AS final
WORKDIR /app
COPY --from=build /build/result .
ENTRYPOINT ["dotnet", "HackathonProblem.Developer.dll"]
