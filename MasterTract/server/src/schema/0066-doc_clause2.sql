update DOC_LEASE_CLAUSE2 set 
    NAME=case when CODE='DEPTH' then 'Depth Clause'
              when CODE='DAMAGE' then 'Damage'
              when CODE='PUGH' then  'Horizontal Pugh Clause'
              when CODE='SHUT_IN_GAS' then 'Vertical Pugh Clause'
              when CODE='TAKE_GAS_ROY' then 'Royalty in Kind'
              when CODE='SURFACE' then 'Surface Use'
              when CODE='CONT_DRILL' then 'Cont Drilling'
              when CODE='FAV_NAT' then 'Favored Nations'
              when CODE='OPT_TO_EXT' then 'Option to Extend'
              when CODE='ASSIGNMENT' then 'Assignment'
              when CODE='PROD_PAYM' then 'Prod Payment'
              when CODE='POOL_PROV' then 'Pooling Provision'
              when CODE='MIN_ROY_PAY' then 'Min Royalty Payment'
              when CODE='RENEWAL_OPT' then 'Renewal Option'
              when CODE='HBP' then 'HBP'
              when CODE='SPC_PROV' then 'Spacing Provision'
              when CODE='LESSER_INT' then 'Lesser Interest'
              when CODE='REWORK_DAYS' then 'Rework Days'
              when CODE='COUNTERPARTS' then 'Counterparts'
              when CODE='OTHER' then 'Other'
              else null
        end
;

insert into sys_version (version) values ('0.0.66');
