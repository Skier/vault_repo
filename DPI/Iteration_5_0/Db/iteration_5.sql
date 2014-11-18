/*****************************************************************************/ 
/* Iteration 5 DB modification script                                        */
/*****************************************************************************/

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- Alter Wireless_Custdata table

IF NOT EXISTS (SELECT * FROM syscolumns WHERE [name] IN ('WebPassword') AND [id] = 
    (SELECT [id] FROM sysobjects WHERE id = object_id(N'Wireless_Custdata') AND OBJECTPROPERTY(id, N'IsTable') = 1))
BEGIN
    ALTER TABLE [Wireless_Custdata] ADD 
    	[WebPassword] [VARCHAR] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL    
END

GO

-- Create SP: spWireless_Custdata_Get_ByPhone

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[spWireless_Custdata_Get_ByPhone]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[spWireless_Custdata_Get_ByPhone]
GO

CREATE PROCEDURE [dbo].[spWireless_Custdata_Get_ByPhone]
    @PhNumber varchar(25)
AS
    SELECT top 1
        [ID],
        [ESN],
        [PhNumber],
        [SubscriberId],
        [NameFirst],
        [NameLast],
        [Addr1],
        [Addr2],
        [City],
        [State],
        [Zip],
        [Email],
        [ContactNumber],
        [WebPassword]
    FROM
        [dbo].[Wireless_Custdata]
    WHERE
        [PhNumber] = @PhNumber
    ORDER BY ID DESC
GO

-- Alter SP: spWireless_Custdata_Get_All

ALTER PROCEDURE [dbo].[spWireless_Custdata_Get_All]
AS
    SELECT TOP 500 
        [ID],
        [ESN],
        [PhNumber],
        [SubscriberId],
        [NameFirst],
        [NameLast],
        [Addr1],
        [Addr2],
        [City],
        [State],
        [Zip],
        [Email],
        [ContactNumber],
        [WebPassword]
    FROM
        [dbo].[Wireless_Custdata]
GO

-- Alter SP: spWireless_Custdata_Get_ByPhoneOrEsn

ALTER PROCEDURE [dbo].[spWireless_Custdata_Get_ByPhoneOrEsn]
    @PhoneOrEsn varchar(25)
AS
    SELECT top 1
        [ID],
        [ESN],
        [PhNumber],
        [SubscriberId],
        [NameFirst],
        [NameLast],
        [Addr1],
        [Addr2],
        [City],
        [State],
        [Zip],
        [Email],
        [ContactNumber],
        [WebPassword]
    FROM
        [dbo].[Wireless_Custdata]
    WHERE
        ((ESN = @PhoneOrEsn) or (PhNumber = @PhoneOrEsn))
    Order by ID Desc
GO

-- Alter SP: spWireless_Custdata_Get_Id

ALTER PROCEDURE [dbo].[spWireless_Custdata_Get_Id]
    @ID int
AS
    SELECT
        [ID],
        [ESN],
        [PhNumber],
        [SubscriberId],
        [NameFirst],
        [NameLast],
        [Addr1],
        [Addr2],
        [City],
        [State],
        [Zip],
        [Email],
        [ContactNumber],
        [WebPassword]
    FROM
        [dbo].[Wireless_Custdata]
    WHERE
        [ID] = @ID
GO

-- Alter SP: spWireless_Custdata_Ins

ALTER PROCEDURE [dbo].[spWireless_Custdata_Ins]
    @ID int OUT,
    @ESN varchar(25),
    @PhNumber varchar(10),
    @SubscriberId varchar(10),
    @NameFirst varchar(50),
    @NameLast varchar(50),
    @Addr1 varchar(200),
    @Addr2 varchar(100),
    @City varchar(100),
    @State char(2),
    @Zip varchar(10),
    @Email varchar(200),
    @ContactNumber varchar(10),
    @WebPassword varchar(25) = NULL
AS    
    BEGIN TRANSACTION  

    INSERT INTO
        [dbo].[Wireless_Custdata]
    (
        [ESN],
        [PhNumber],
        [SubscriberId],
        [NameFirst],
        [NameLast],
        [Addr1],
        [Addr2],
        [City],
        [State],
        [Zip],
        [Email],
        [ContactNumber],
        [WebPassword]
    )
    VALUES
    (
        @ESN,
        @PhNumber,
        @SubscriberId,
        @NameFirst,
        @NameLast,
        @Addr1,
        @Addr2,
        @City,
        @State,
        @Zip,
        @Email,
        @ContactNumber,
        @WebPassword
    )
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
    SELECT @ID= CAST(SCOPE_IDENTITY() AS int)
    
    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION
    RETURN(0)
    
    HandleError:
    IF @@TRANCOUNT > 1
        COMMIT TRANSACTION
    ELSE IF @@TRANCOUNT = 1
        ROLLBACK TRANSACTION
    
    RAISERROR(50002, 16, 1)
    RETURN(1)
GO

-- Alter SP: spWireless_Custdata_Upd

ALTER PROCEDURE [dbo].[spWireless_Custdata_Upd]
    @ID int,
    @ESN varchar(25),
    @PhNumber varchar(10),
    @SubscriberId varchar(10),
    @NameFirst varchar(50),
    @NameLast varchar(50),
    @Addr1 varchar(200),
    @Addr2 varchar(100),
    @City varchar(100),
    @State char(2),
    @Zip varchar(10),
    @Email varchar(200),
    @ContactNumber varchar(10),
    @WebPassword varchar(25) = NULL
AS    
    BEGIN TRANSACTION    

    UPDATE
        [dbo].[Wireless_Custdata]
    SET
        [ESN] = @ESN,
        [PhNumber] = @PhNumber,
        [SubscriberId] = @SubscriberId,
        [NameFirst] = @NameFirst,
        [NameLast] = @NameLast,
        [Addr1] = @Addr1,
        [Addr2] = @Addr2,
        [City] = @City,
        [State] = @State,
        [Zip] = @Zip,
        [Email] = @Email,
        [ContactNumber] = @ContactNumber,
        [WebPassword] = @WebPassword
    WHERE
        [ID] = @ID
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION
    RETURN(0)
    
    HandleError:
    IF @@TRANCOUNT > 1
        COMMIT TRANSACTION
    ELSE IF @@TRANCOUNT = 1
        ROLLBACK TRANSACTION
    
    RAISERROR(50002, 16, 1)
    RETURN(1)   
GO

-- CreditCard_Payment table

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[FK_CreditCard_Payment_CustData]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1)
    ALTER TABLE [dbo].[CreditCard_Payment] 
        DROP CONSTRAINT [FK_CreditCard_Payment_CustData]
GO

-- Account_Notes table

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[FK_Account_Notes_CustData]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1)
    ALTER TABLE [dbo].[Account_Notes] 
        DROP CONSTRAINT [FK_Account_Notes_CustData]
GO

-- Create spWirelessGetFinalProductsBySubProduct

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[spWirelessGetFinalProductsBySubProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[spWirelessGetFinalProductsBySubProduct]
GO

CREATE PROCEDURE [dbo].[spWirelessGetFinalProductsBySubProduct]
    @SubProductId INT
AS
    SELECT 
        [wp].[Wireless_product_id],
        [wp].[Product_name] + '  $' + Convert(Varchar(6), [wp].[Price]) [Product_name],
        [wp].[UniProdName],
        [wp].[Supplier_id],
        [wp].[Vendor_id],
        [wp].[Soc],
        [wp].[Expiration],
        [wp].[Price],
        [wp].[Start_date],
        [wp].[End_date],
        [wp].[Receipt_text],
        [wp].[Product_commission_percent],
        [wp].[Product_commission_flat],
        [wp].[ProdId],
        [wp].[OverrideWSProvider],
        [wp].[IsActivationReq],
        [wp].[IsPhoneReq],
        [wp].[ReqItems],
        [wp].[IsXml],
        [wp].[IsPerValidationReq],
    	[wp].[TaxCode]
    FROM [dbo].[wireless_products] [wp] 
    WHERE [wireless_product_id] IN 
    (
        SELECT [wireless_product_id]
        FROM [dbo].[wireless_products_composition]
        WHERE [wireless_subprod_id] = @SubProductId
    )
GO

-- Create spWirelssGetSubProductsByFinalProduct

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[spWirelssGetSubProductsByFinalProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[spWirelssGetSubProductsByFinalProduct]
GO

CREATE PROCEDURE [dbo].[spWirelssGetSubProductsByFinalProduct]
    @FinalProductId INT
AS 
    SELECT 
        [wp].[Wireless_product_id],
        [wp].[Product_name] + '  $' + Convert(Varchar(6), [wp].[Price]) [Product_name],
        [wp].[UniProdName],
        [wp].[Supplier_id],
        [wp].[Vendor_id],
        [wp].[Soc],
        [wp].[Expiration],
        [wp].[Price],
        [wp].[Start_date],
        [wp].[End_date],
        [wp].[Receipt_text],
        [wp].[Product_commission_percent],
        [wp].[Product_commission_flat],
        [wp].[ProdId],
        [wp].[OverrideWSProvider],
        [wp].[IsActivationReq],
        [wp].[IsPhoneReq],
        [wp].[ReqItems],
        [wp].[IsXml],
        [wp].[IsPerValidationReq],
    	[wp].[TaxCode]
    FROM [dbo].[wireless_products] [wp] 
    WHERE [wireless_product_id] IN 
    (
        SELECT [wireless_subprod_id]
        FROM [dbo].[wireless_products_composition]
        WHERE [wireless_product_id] = @FinalProductId
    )
GO

-- Create spAOL_IsPinAvailable

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[spAOL_IsPinAvailable]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[spAOL_IsPinAvailable]
GO

CREATE PROCEDURE [dbo].[spAOL_IsPinAvailable]
    @WirelessProductId INT
AS
    SELECT COUNT(*) 
    FROM [dbo].[AOL_pins] 
    WHERE [active] = 0 
        AND [wireless_product_id] = @WirelessProductId 
    GROUP BY [wireless_product_id]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO