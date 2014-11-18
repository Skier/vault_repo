insert into Warehouse (WarehouseId, WarehouseName, Address1, Address2, City, State, Zip, Country, WarehouseCode) 
values (1, 'N. Carolina','3301 N Main St', '', 'Tarboro', 'NC', '27886', 'USA', 'NCD');

insert into Warehouse (WarehouseId, WarehouseName, Address1, Address2, City, State, Zip, Country, WarehouseCode) 
values (2, 'Fridley', '99 Northeast 77th Way', '', 'Fridley', 'MN', '55432', 'USA', '*FRD');

insert into Warehouse (WarehouseId, WarehouseName, Address1, Address2, City, State, Zip, Country, WarehouseCode) 
values (3, 'Sunnyvale', '1393 Barregas Avenue', '', 'Sunnyvale', 'CA', '94089', 'USA', '*SNV');
go



insert into ShippingType (ShippingTypeId , ShippingType, ShippingCode ) values ( 1, 'Overnight'   , 'NDA');
insert into ShippingType (ShippingTypeId , ShippingType, ShippingCode ) values ( 2, '2 Day Air'   , '2DA');
insert into ShippingType (ShippingTypeId , ShippingType, ShippingCode ) values ( 3, '3 Day Air'   , '3-4');
insert into ShippingType (ShippingTypeId , ShippingType, ShippingCode ) values ( 4, 'Ground'      , '3-4G');
go

insert into OrderStatus ( OrderStatusId, Status ) values ( -1, 'Draft');
insert into OrderStatus ( OrderStatusId, Status ) values ( 1, 'Submitted');
insert into OrderStatus ( OrderStatusId, Status ) values ( 2, 'Confirmed');
go

insert into Brand (BrandId, Code, BrandName) values (1, 'TTS', 'Titus');
go

insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (2, 0, 150);
insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (3, 151, 300);
insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (4, 301, 600);
insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (5, 601, 1000);
insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (6, 1001, 1400);
insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (7, 1401, 1800);
insert into ShippingZones (ZoneId, StartDistance, EndDistance) values (8, 1801, 40000);
go



