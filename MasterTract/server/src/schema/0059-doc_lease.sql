create or replace function DOC_LEASE_EXP_DATE(EFFECT_DATE date, TERM integer) 
    returns date
    stable 
as 
$$
declare 
    RESULT date;
begin 
    select into RESULT (EFFECT_DATE + (TERM || ' months')::interval);
    return RESULT;
end;
$$ 
language 'plpgsql';

insert into sys_version (version) values ('0.0.59');
