#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Mohajer.ClassScheduleProject.Migrator/Mohajer.ClassScheduleProject.Migrator.csproj", "src/Mohajer.ClassScheduleProject.Migrator/"]
COPY ["src/Mohajer.ClassScheduleProject.EntityFrameworkCore/Mohajer.ClassScheduleProject.EntityFrameworkCore.csproj", "src/Mohajer.ClassScheduleProject.EntityFrameworkCore/"]
COPY ["src/Mohajer.ClassScheduleProject.Core/Mohajer.ClassScheduleProject.Core.csproj", "src/Mohajer.ClassScheduleProject.Core/"]
COPY ["src/Mohajer.ClassScheduleProject.Core.Shared/Mohajer.ClassScheduleProject.Core.Shared.csproj", "src/Mohajer.ClassScheduleProject.Core.Shared/"]
RUN dotnet restore "src/Mohajer.ClassScheduleProject.Migrator/Mohajer.ClassScheduleProject.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Mohajer.ClassScheduleProject.Migrator"
RUN dotnet build "Mohajer.ClassScheduleProject.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mohajer.ClassScheduleProject.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mohajer.ClassScheduleProject.Migrator.dll"]
