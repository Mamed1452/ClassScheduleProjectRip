#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Mohajer.ClassScheduleProject.Web.Host/Mohajer.ClassScheduleProject.Web.Host.csproj", "src/Mohajer.ClassScheduleProject.Web.Host/"]
COPY ["src/Mohajer.ClassScheduleProject.Web.Core/Mohajer.ClassScheduleProject.Web.Core.csproj", "src/Mohajer.ClassScheduleProject.Web.Core/"]
COPY ["src/Mohajer.ClassScheduleProject.Application/Mohajer.ClassScheduleProject.Application.csproj", "src/Mohajer.ClassScheduleProject.Application/"]
COPY ["src/Mohajer.ClassScheduleProject.Application.Shared/Mohajer.ClassScheduleProject.Application.Shared.csproj", "src/Mohajer.ClassScheduleProject.Application.Shared/"]
COPY ["src/Mohajer.ClassScheduleProject.Core.Shared/Mohajer.ClassScheduleProject.Core.Shared.csproj", "src/Mohajer.ClassScheduleProject.Core.Shared/"]
COPY ["src/Mohajer.ClassScheduleProject.Core/Mohajer.ClassScheduleProject.Core.csproj", "src/Mohajer.ClassScheduleProject.Core/"]
COPY ["src/Mohajer.ClassScheduleProject.EntityFrameworkCore/Mohajer.ClassScheduleProject.EntityFrameworkCore.csproj", "src/Mohajer.ClassScheduleProject.EntityFrameworkCore/"]
COPY ["src/Mohajer.ClassScheduleProject.GraphQL/Mohajer.ClassScheduleProject.GraphQL.csproj", "src/Mohajer.ClassScheduleProject.GraphQL/"]
RUN dotnet restore "src/Mohajer.ClassScheduleProject.Web.Host/Mohajer.ClassScheduleProject.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Mohajer.ClassScheduleProject.Web.Host"
RUN dotnet build "Mohajer.ClassScheduleProject.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mohajer.ClassScheduleProject.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mohajer.ClassScheduleProject.Web.Host.dll"]
