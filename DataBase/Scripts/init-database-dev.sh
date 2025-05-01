#!/bin/bash
echo "Aguarde até a execução do DER"
echo "Aguardando serviço sqlserver ficar pronto."
sleep 5
echo "Executando der"

echo "Executando part 1"
/opt/mssql-tools/bin/sqlcmd -S sqlserver-revendadeveiculos -U sa -P SqlServer2019! -d master -i /tmp/tech-challenge-mer-1.sql
exit_code=$?
if [ $exit_code -eq 0 ]; then
    echo "Executando part 2"
    sleep 5
    /opt/mssql-tools/bin/sqlcmd -S sqlserver-revendadeveiculos -U sa -P SqlServer2019! -d tech-challenge-revenda-de-veiculos -i /tmp/tech-challenge-mer-2.sql
    echo "DER executado :: Successfully " $exit_code
else
for (( i = 1; i <= 3; i++ ))
do 
    sleep 5
    echo "Executando der :: Nova tentativa :: " $i
    echo "Executando part 1"
    /opt/mssql-tools/bin/sqlcmd -S sqlserver-revendadeveiculos -U sa -P SqlServer2019! -d master -i /tmp/tech-challenge-mer-1.sql
    exit_code=$?
    if [ $exit_code -eq 0 ]; then
        echo "Executando part 2"
        sleep 5
        /opt/mssql-tools/bin/sqlcmd -S sqlserver-revendadeveiculos -U sa -P SqlServer2019! -d tech-challenge-revenda-de-veiculos -i /tmp/tech-challenge-mer-2.sql
        echo "DER executado :: Successfully " $?
        break
    fi
done
fi
