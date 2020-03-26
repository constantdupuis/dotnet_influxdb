# dotnet_influxdb

Play with dotnet and influxdb.

## Prerequisites

You should be familiare with Docker, docker-compose, dotnet and InfluxDB.

## Setup

Install dotnet core >= [3.1](https://dotnet.microsoft.com/download/dotnet-core/current), Install [Docker Desktop](https://www.docker.com/products/docker-desktop). You migth need to install `docker-compose` separatly depending on your pltaform.

Clone this repo in a empty folder.

`> git clone https://github.com/constantdupuis/dotnet_influxdb.git .`

In this directory restore rependencies

`> dotnet restore`

Then start docker-compose:

```PowerShell
> docker-compose up
```

This will start one instance of

- InfluxDB
- Grafana
- Chronograf

All data of the containes are stored in folder `data`.

See `docker-compose.yaml` for more information on ports and login/password.

Now run the programm

`> dotnet run`

## To query InfluxDB

While docker-compose is running, run the following command to get access to the influx CLI.

`> docker exec -it <parent_folder_name>_influxdb_1 influx`

# InfluxQL

## Common issues

### Tags can only be STRING

> Tags can only contains string, so no mathematical operations are allowed on them

eg: `SUM(tag)` **NOT ALLOWED**

### String comparaison

> In WHERE clause the string used for comparaison MUST be delimited by SINGLE QUOTES

eg: `WHERE "project-name" = 'PROF01'`

See [this article](https://docs.influxdata.com/influxdb/v1.7/query_language/data_exploration/#a-where-clause-query-unexpectedly-returns-no-data)

### GROUP BY

You can only GOUR BY tags!
