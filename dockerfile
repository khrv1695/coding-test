# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy the full source code (not just csproj)
COPY ./src .

# 2. (Optional) Copy solution file if exists
# COPY coding-test.sln .

# 3. Then restore
WORKDIR /src/API
RUN dotnet restore

# 4. Now publish
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
