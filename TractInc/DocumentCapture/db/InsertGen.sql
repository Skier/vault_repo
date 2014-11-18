select 'insert into states(objectid, state_id, state_Name, state_fips) values(' +  str([objectid]) + ',' +  str(state_id) + ',''' + state_Name + ''',''' +  state_fips + ''')'
from states

select 'insert into countys(objectid, state_id, [Name],state_Name, STATE_FIPS, CNTY_FIPS, FIPS) values(' +  str([objectid]) + ',' +  str(state_id) +',''' + replace([Name], '''','''''')  + ''',''' + replace(state_Name, '''', '''''') + ''',''' +  state_fips + ''',''' +  cnty_fips + ''',''' +  fips + + ''')'
from countys


select 'insert into documenttype([Name], [SellerRole], [BuyerRole]) values(''' + [Name] +''',''' + [SellerRole] +''','''+ [BuyerRole] + ''')' 
from documenttype