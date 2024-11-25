FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /build
COPY HackathonProblem.HrManager HackathonProblem.HrManager
COPY HackathonProblem.Common HackathonProblem.Common
COPY HackathonProblem.CsvEmployeeProvider HackathonProblem.CsvEmployeeProvider
COPY HackathonProblem.RandomWishlistsProvider HackathonProblem.RandomWishlistsProvider
RUN dotnet restore ./HackathonProblem.HrManager/HackathonProblem.HrManager.csproj
RUN dotnet publish ./HackathonProblem.HrManager/HackathonProblem.HrManager.csproj -c Release -o result

FROM base AS final
WORKDIR /app
COPY --from=build /build/result .
ENTRYPOINT ["dotnet", "HackathonProblem.HrManager.dll", "--urls=http://0.0.0.0:5000"]
