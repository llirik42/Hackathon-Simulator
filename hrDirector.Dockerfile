FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /build
COPY . .
RUN dotnet restore ./HackathonProblem.HrDirector/HackathonProblem.HrDirector.csproj
RUN dotnet publish ./HackathonProblem.HrDirector/HackathonProblem.HrDirector.csproj -c Release -o result

FROM base AS final
WORKDIR /app
COPY --from=build /build/result .
ENTRYPOINT ["dotnet", "HackathonProblem.HrDirector.dll", "--urls=http://0.0.0.0:5001"]
