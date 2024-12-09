FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /build
COPY . .
RUN dotnet restore ./HackathonProblem.HrManager/HackathonProblem.HrManager.csproj
RUN dotnet publish ./HackathonProblem.HrManager/HackathonProblem.HrManager.csproj -c Release -o result

FROM base AS final
WORKDIR /app
COPY --from=build /build/result .
ENTRYPOINT ["dotnet", "HackathonProblem.HrManager.dll"]
