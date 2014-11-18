cd "C:\Program Files\MySQL\MySQL Tools for 5.0\"
grtsh.exe -x C:\Work\Dalworth\Dalworth.Server\Database\migration.lua
cd C:\Work\Dalworth\Dalworth.Server\Database\
"C:\Program Files\MySQL\MySQL Server 5.0\bin\mysql" --user=root --password=++Winston --database=dalworth_server_dbo < db_code.sql
UpdateScripts.bat
