FROM mcr.microsoft.com/mssql/server:2022-latest

ENV MSSQL_SA_PASSWORD=P@ssw0rd
ENV ACCEPT_EULA=Y

USER root

RUN mkdir -p /opt/scripts

COPY ./ChatApp.API/Database/create_database.sql /opt/scripts/
COPY ./ChatApp.API/Database/create_tables.sql /opt/scripts/
COPY ./ChatApp.API/Database/insert_seed_data.sql /opt/scripts/

USER mssql

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 45 && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -d master -i /opt/scripts/create_database.sql -C && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -d master -i /opt/scripts/create_tables.sql -C && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -d master -i /opt/scripts/insert_seed_data.sql -C

EXPOSE 1433