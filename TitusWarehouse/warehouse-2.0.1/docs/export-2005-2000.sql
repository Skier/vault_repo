select 
    'insert into MacPac_Inventory (Plant, Brand, Part,  PartDesc, '
    +'    AltDesc, Model, Configuration, ContainerCode, Height, Depth, Width, ContainerWeight, '
    +' partweight, qtypercontainer, BasePrice, OnHand, Allocated, PartStatus, MacPacTimeStamp, ImportTimeStamp )'
    +' values (''' + Plant +''','''+ Brand +''','''+ Part +''','''+  PartDesc +''','''
    +      AltDesc +''','''+ Model +''','''+ Configuration +''','''+ ContainerCode +''','
    +      str(Height) +','+ str(Depth) +','+ str(Width) +','+ str(ContainerWeight) +',' 
    +      str(partweight) +','+ str(qtypercontainer) +','+ str(BasePrice) +',' 
    +      str(OnHand) +','+ str(Allocated) +','''+ PartStatus +''', getdate(), getdate() ) ' 
from MacPac_Inventory


SELECT 
' insert into macpac_multiplier_header (brand, customer_id,marketing_program,lastupdatedate ) values ( '''
+brand+''','''+customer_id+''','''+marketing_program+''', getdate() )' 
  FROM macpac_multiplier_header

select 
'INSERT INTO Macpac_Multiplier ([brand_code],[marketing_program],[part],[Desc],[multiplier],[LastUpdateDate]) ' +
' VALUES ( '''+brand_code+''',''' +marketing_program +''','''+part+''','''+[Desc]+''','+str(multiplier)+', getdate())'
from Macpac_Multiplier