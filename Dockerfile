FROM microsoft/dotnet:sdk AS development
WORKDIR /app

COPY *.csproj .
RUN dotnet restore
CMD ["dotnet", "watch", "run"]

COPY . .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=development /app/out .
ENTRYPOINT ["dotnet", "api.dll"]