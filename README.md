# My World Clock

My world clock, allowing customised clocks in different timezones.

## Overview

- `MyWorldClock` uses [NodaTime](https://nodatime.org/) to:
  - implement a `ClockService` that exposes different properties/methods to allow the caller to make the correct use of the different date/time/timezone types
  - implement a `WorldClockService` that generates the time of day in different timezones
- `MyWorldClock.Tests` contains unit tests for the above
- `MyWorldClock.Functions` exposes parts of the services above as Azure Functions to allow their use via REST endpoints and uses the NodaTime serialisation packages
- `MyWorldClock.Web` uses the web api to render SVG clocks, powered by the `WorldClockService`

## To deploy the application

- Login into the Azure Portal
- Create a new "Static Web App"
- Choose/create a resource group
- Enter a name
- Choose a region
- Under Deployment Details:
  - Choose `GitHub` deployment
  - Select the details for this repo
- Under Build Details:
  - `Build Presets` = `Angular`
  - `App location` = `/MyWorldClock.Web`
  - `Api location` = `/MyWorldClock.Functions`
  - `Output location` = `dist`

Create the static web app and wait for the deployment to complete. 
You can view the progress via the links on the Overview section of the static web app.

<!--
## Pre-requisites
Ensure you have the following:
* The [Terraform CLI](/tutorials/terraform/install-cli?in=terraform/aws-get-started) (1.4.6+) installed.
* [An Azure account](https://azure.microsoft.com/).
* The [Azure CLI (2.48.0+)](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) installed.

## To deploy the application

Open a **Powershell** prompt at the project folder, i.e. the folder than contains the `main.tf` file.

Log in to your Azure account:
```
az login
```
Use the browser that is opened to authenticate to your Azure account. Your `~/.azure` profile will be updated with the required authentication tokens.
Close the browser.

Build/publish the application:
```
./publish.ps1
```

Initialise TerraForm:
```
terraform init
```

View the deployment plan:
```
terraform plan
```

Deploy the application:
```
terraform apply
```
When prompted, enter `yes` to confirm the deployment.

Once complete, copy/paste the `function_app_url` output value from the console into your browser.
-->

## To run locally

### Functions app

Create the following `local.settings.json` file in the `MyWorldClock.Functions` folder

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

- Open a prompt in the `MyWorldClock.Functions` folder
- Run the Functions app project
```
func start
```
- The available endpoints and listed in the terminal output and sit under https://localhost:7103/api/

### Angular app

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
- [x] Language Support
  - [x] Display a list of language codes to choose from
  - [x] Default the language to the browsers' current language
  - [x] Upon changing the language:
    - [X] Change the language of the timezone dropdown lists
    - [x] Change the language of the displayed clocks
- [x] Default the timezone dropdowns to the browsers' current timezone 
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
