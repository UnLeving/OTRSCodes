FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["otrsCode_netCore/otrsCode_netCore.csproj", "otrsCode_netCore/"]
RUN dotnet restore "otrsCode_netCore/otrsCode_netCore.csproj"
COPY . .
WORKDIR "/src/otrsCode_netCore"
RUN dotnet build "otrsCode_netCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "otrsCode_netCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "otrsCode_netCore.dll"]