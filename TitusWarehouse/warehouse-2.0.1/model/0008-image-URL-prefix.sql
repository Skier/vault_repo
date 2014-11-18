alter table Brand
   add  ImageURLPrefix  varchar(255)  default 'http://';
go

update Brand 
   set ImageURLPrefix='http://www.titus-hvac.com/ecatalog/getimage2.aspx?ref='
 where BrandId = 1;
go

update Brand 
   set ImageURLPrefix='http://www.krueger-hvac.com/ecatalog/getimage2.aspx?ref='
 where BrandId = 2;
go