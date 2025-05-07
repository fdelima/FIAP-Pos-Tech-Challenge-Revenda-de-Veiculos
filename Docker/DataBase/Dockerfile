FROM mcr.microsoft.com/mssql-tools
ARG INSTANCE
ARG USER
ARG PWD
COPY ["./DataBase/Scripts/init-database.sh", "./tmp/init-database.sh"]
COPY ["./DataBase/Modelagem-banco-de-dados/tech-challenge-mer-1.sql", "./tmp/tech-challenge-mer-1.sql"]
COPY ["./DataBase/Modelagem-banco-de-dados/tech-challenge-mer-2.sql", "./tmp/tech-challenge-mer-2.sql"]
ENTRYPOINT ["/bin/bash", "./tmp/init-database.sh", "${INSTANCE}", "${USER}", "${PWD}"]