# My World Clock

My world clock, allowing customised clocks in different timezones.

## Overview

- `MyWorldClock` uses [NodaTime](https://nodatime.org/) to:
  - implement a `ClockService` that exposes different properties/methods to allow the caller to make the correct use of the different date/time/timezone types
  - implement a `WorldClockService` that generates the time of day in different timezones
- `MyWorldClock.Tests` contains unit tests for the above
- `MyWorldClock.Functions` exposes parts of the services above as Azure Functions to allow their use via REST endpoints and uses the NodaTime serialisation packages
- `MyWorldClock.Web` uses the web api to render SVG clocks, powered by the `WorldClockService`

## Sample `local.settings.json` for `MyWorldClock.Functions` Project

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
    },
    "Host": {
        "LocalHttpPort": 7071,
        "CORS": "*"
    }
}
```

## Running the web app
- Open a prompt in the root of the repo
- Run the web api project
```
dotnet run --project ./MyWorldClock.WebApi/MyWorldClock.WebApi.csproj --launch-profile https
```
- (you can view the OpenApi page at https://localhost:7103/swagger/index.html)


- Open another prompt in the root of the repo
- Run the web project
```
dotnet run --project ./MyWorldClock.Web/MyWorldClock.Web.csproj --launch-profile https
```
- Open your browser to https://localhost:7049/

## To Do
- [ ] Deployment
  - [x] Change web api project to use Azure Functions
  - [ ] Change web project to be an ASP.NET Core web app hosting Angular SPA
  - [ ] Deploy to Azure using Terraform 
- [ ] Security (using AzureAD)
  - [ ] Sign-up via Google
  - [ ] Login via web site
  - [ ] Secure the web api
  - [ ] Sign-up via other accounts
- [ ] Profiles
  - [ ] Create a profile with a name and associate to a login
  - [ ] Add multiple clocks to a profile with a chosen timezone
  - [ ] Create multiple profiles and switch between them
  - [ ] In-memory profile store
  - [ ] NoSql profile store (using CosmosDB)
- [ ] Adding clocks
  - [ ] Current time and timezone (implicit)
  - [ ] From current user? From server? From browser?
  - [ ] Different time and current timezone (implicit)
  - [ ] Different time and different timezone
  - [ ] Current time (implicit) and different timezone
