osql -U sa -P gfhjkm -d ascwarehouse -i create-import-table.sql 
bcp ascwarehouse.dbo.tmp_titus in titus-prepared.csv -S (local) -U sa -P gfhjkm -f import.fmt
bcp ascwarehouse.dbo.tmp_titus_items in titus-items.csv -S (local) -U sa -P gfhjkm -f import-items.fmt
osql -U sa -P gfhjkm -d ascwarehouse -i fill-data.sql
osql -U sa -P gfhjkm -d ascwarehouse -i drop-import-table.sql 
