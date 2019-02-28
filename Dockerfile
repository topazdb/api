FROM microsoft/dotnet:sdk AS development
WORKDIR /app

COPY src/*.csproj .
RUN dotnet restore
CMD ["dotnet", "watch", "run"]

COPY src .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=development /app/out .
ENTRYPOINT ["dotnet", "api.dll"]