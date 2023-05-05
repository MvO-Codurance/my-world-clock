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
- Open a prompt in the `MyWorldClock.Functions` folder
- Run the Functions app project
```
func start
```
- The available endpoints and listed in the terminal output and sit under https://localhost:7103/api/


- Open another prompt in the `MyWorldClock.Web` folder
- Serve the Angular project
```
ng serve --open
```
- Open your browser to https://localhost:4200/

## To Do
- [x] Refactoring
  - [x] Change web api project to use Azure Functions
  - [x] Change web project to be a plain Angular SPA
- [ ] Language Support
  - [ ] Display a list of language codes to choose from
  - [ ] Default the chosen language to the browsers' current language
  - [ ] Upon changing the language:
    - [ ] Change the language of the timezone dropdown lists
    - [ ] Change the language of the displayed clocks
- [ ] Deployment
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
