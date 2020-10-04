#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE USER borgun WITH PASSWORD 'salt';
    GRANT ALL PRIVILEGES ON DATABASE test TO borgun;
EOSQL