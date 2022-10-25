#!/bin/bash

/opt/mssql-tools/bin/sqlcmd -S $1 -l 20000 -U $2 -P $3 -Q "CREATE DATABASE $4" 

echo napravio bazu

/opt/mssql-tools/bin/sqlcmd -S $1 -l 20000 -U $2 -P $3 -d "$4" -i /opt/mssql-scripts/init_db.sql

echo zavrsio migraciju