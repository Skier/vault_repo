/*==============================================================*/
/* Table: ShippingMultiplier                                    */
/*==============================================================*/
create table ShippingMultiplier (
   BrandId              integer              not null,
   ShippingTypeId       integer              not null,
   CostGreateThan       money                not null,
   CostLessOrEqualThan  money                not null,
   Multiplier           double precision     not null, 
   Country              varchar(2)           not null default 'US'
)
go


alter table ShippingMultiplier
   add constraint PK_SHIPPINGMULTIPLIER primary key  (BrandId, ShippingTypeId, Country, CostGreateThan, CostLessOrEqualThan)
go


alter table ShippingMultiplier
   add constraint FK_SHIPPING_REFERENCE_SHIPPING foreign key (ShippingTypeId)
      references ShippingType (ShippingTypeId)
go


alter table ShippingMultiplier
   add constraint FK_SHIPPING_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go



--alter table ShippingMultiplier
--   add Country              varchar(2)           not null default 'US'
--go

--alter table ShippingMultiplier
--   drop constraint PK_SHIPPINGMULTIPLIER
--go 

--alter table ShippingMultiplier
--   add constraint PK_SHIPPINGMULTIPLIER primary key  (BrandId, ShippingTypeId, Country, CostGreateThan, CostLessOrEqualThan)
--go


create table zips (
   country varchar(2)   not null, 
   zip     varchar(10)  not null, 
   state   varchar(2)   not null, 
   city    varchar(255) not null
)
go 

create  index zips_idx on zips (country, zip)
go 


update Warehouse set Country = 'US';

create table FedexRates (
    ShipmentId       integer              not null, 
    FedexServiceType varchar(100)         not null, 
    FedexRateType    varchar(100)         not null, 
    FedexBaseCharge  decimal(15,2)        not null, 
    FedexDiscount    decimal(15,2)        not null, 
    FedexSurcharges  decimal(15,2)        not null, 
    FedexNetCharge   decimal(15,2)        not null, 
)
go

alter table FedexRates
   add constraint PK_FedexRates primary key  (ShipmentId, FedexServiceType, FedexRateType)
go

alter table ShoppingCartShipment 
   add ShippingMultiplier double precision null
go



insert into ShippingType (ShippingTypeId , ShippingType, ShippingCode ) values ( 5, 'Int''l Next Day'      , 'NDA')
go

insert into ShippingType (ShippingTypeId , ShippingType, ShippingCode ) values ( 6, 'Int''l 2-3 Days'      , '2DA')
go




/****** Object:  Table [dbo].[PackageCalcLog]    Script Date: 02/04/2009
09:57:31 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

CREATE TABLE [dbo].[PackageCalcLog](

      [PackageCalcLogID] [int] IDENTITY(1,1) NOT NULL,

      [ShoppingCartID] [int] NULL,

      [CalcXML] [text] NULL,

      [LastUpdateDate] [datetime] NULL CONSTRAINT
[DF_PackageCalcLog_LastUpdateDate]  DEFAULT (getdate())

) ON [PRIMARY]

GO

SET ANSI_PADDING OFF

GO

/****** Object:  StoredProcedure [dbo].[getitemsdetail]    Script Date:
02/04/2009 09:57:30 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

 

 

GO

-- =============================================

-- Author:        <Author,,Name>

-- Create date: <Create Date,,>

-- Description:   <Description,,>

-- =============================================

CREATE PROCEDURE [dbo].[getitemsdetail]

            @xml as text

AS

BEGIN

--declare @xml as varchar(4000)

--set @xml = '<root><sku><id>20015</id></sku><sku><id>20029</id></sku></root>'

declare @dochandle int

exec sp_xml_preparedocument @dochandle output, @xml

--insert into @temp

select SKU, weight,qtyincrement from item

where SKU in

(

select * from openxml(@dochandle,'root/sku',3)

with (id varchar(10))

)

EXEC sp_xml_removedocument @dochandle

 

END

GO

/****** Object:  StoredProcedure [dbo].[InsertPackageCalcLog]    Script
Date: 02/04/2009 09:57:30 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

-- =============================================

-- Author:        <Author,,Name>

-- Create date: <Create Date,,>

-- Description:   <Description,,>

-- =============================================
--drop procedure [dbo].[InsertPackageCalcLog]
--go

CREATE PROCEDURE [dbo].[InsertPackageCalcLog]

      @ShoppingCartId  int,

      @CalcXml    text
AS

BEGIN

      

      insert into packagecalclog

    (shoppingcartid,calcxml)

values (@ShoppingCartId,@CalcXml)

 

END

GO