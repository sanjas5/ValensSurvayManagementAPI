# M2 Docker Compose Setup for SQL Server

This repo contains the docker-compose.yaml, and other data that is needed to setup a sql server container and data on a Mac M2 processor

To customize the database metadata change the current values to the values that you want:

```bash
password      ~> MSSQLTESTPASSWORD123$$
database name ~> TEST_DATABASE

```

To add additional setup data, change the init.sql files contents
