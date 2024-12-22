
# Energy Price checker

A background service to check energy prices of last 24 hours for every hour and save into a SqlLite database




## Run Locally

Clone the project

```bash
  git clone https://github.com/mohamedalijinnah/background-worker.git
```

Install dependencies

```bash
  dotnet build
```

Create migration

```bash
  dotnet ef migrations add <migration name> --project EnergyPriceChecker
```

Apply migration

```bash
  dotnet ef database update <migration name> --project EnergyPriceChecker
```


Start the service

```bash
  dotnet run
```

## Description

EnergyPricesJob is a background service, scheduled to run every 24 hours to fetch the electricity and gas prices for last 24 hours



## Authors

- [@mohamedalijinnah](https://www.github.com/mohamedalijinnah)

