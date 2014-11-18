/*****************************************************************************/ 
/* Iteration 2 DB modification script                                        */
/*****************************************************************************/

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

-- CustData table

UPDATE [CustData]
SET [WebPassword] = '0' + [WebPassword]
WHERE [WebPassword] IS NOT NULL
GO

-- spStoreLocation_Get_States

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStoreLocation_Get_States]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spStoreLocation_Get_States]
GO

CREATE PROCEDURE [dbo].[spStoreLocation_Get_States]
AS
    SELECT DISTINCT [State]
    FROM [StoreLocation]
    ORDER BY [State]
GO

GRANT EXECUTE
ON spStoreLocation_Get_States
TO webcentral_public_sa
GO

-- spStoreLocation_Get_CitiesByState

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStoreLocation_Get_CitiesByState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spStoreLocation_Get_CitiesByState]
GO

CREATE PROCEDURE [dbo].[spStoreLocation_Get_CitiesByState]
    @State CHAR(2)
AS
    SELECT DISTINCT [City]
    FROM [StoreLocation]
    WHERE [State] = @State AND [City] IS NOT NULL
    ORDER BY [City]
GO

GRANT EXECUTE
ON spStoreLocation_Get_CitiesByState
TO webcentral_public_sa
GO

-- spStoreLocation_Get_AllByStateAndCity

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStoreLocation_Get_AllByStateAndCity]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spStoreLocation_Get_AllByStateAndCity]
GO

CREATE PROCEDURE [dbo].[spStoreLocation_Get_AllByStateAndCity]
    @State CHAR(2),
    @City  VARCHAR(50)
AS
    SELECT 
        [StoreCode],
        [StoreClass],
        [Name],
        [StoreNumber],
        [Address],
        [City],
        [State],
        [Zip],
        [Phone],
        [Fax],
        [Manager],
        [Active],
        [ActiveDate],
        [PriceCode],
        [Wireless_PriceCode],
        [Notes],
        [AddLocInf],
        [TermDate],
        [Status],
        [Ilec],
        [DMA],
        [CorpID],
        [Type],
        [Internet_Channel_ID],
        [LocalService],
        [Wireless],
        [Internet],
        [SmartConnect],
        [NET_FlatRate],
        [SC_FlatRate],
        [LS_FlatRate],
        [Divisional_Manager],
        [OverrideDebCardProd],
        [DebitCard],
        [OvrdRestProd],
        [IsNarrowPrinter],
        [Satellite],
        [ShowSource],
        [DpiWireless]
    FROM [StoreLocation]
    WHERE [State] = @State AND [City] = @City
    ORDER BY [Name]
GO

GRANT EXECUTE
ON spStoreLocation_Get_AllByStateAndCity
TO webcentral_public_sa
GO

-- spAccount_CheckPTPByAccNumberAndDueDate

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAccount_CheckPTPByAccNumberAndDueDate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spAccount_CheckPTPByAccNumberAndDueDate]
GO

CREATE PROCEDURE [dbo].[spAccount_CheckPTPByAccNumberAndDueDate]
	@AccNumber INT,
    @DueDate DATETIME
AS
    SELECT COUNT(1)
    FROM Account_PTP
    WHERE AccNumber = @AccNumber AND PTP_Date > @DueDate        
GO

GRANT EXECUTE
ON spAccount_CheckPTPByAccNumberAndDueDate
TO webcentral_public_sa
GO

-- spAccount_GetPTPById

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAccount_GetPTPById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spAccount_GetPTPById]
GO

CREATE PROCEDURE [dbo].[spAccount_GetPTPById]
	@Id INT
AS
    SELECT *
    FROM Account_PTP
    WHERE Account_PTP_ID = @Id    
GO

GRANT EXECUTE
ON spAccount_GetPTPById
TO webcentral_public_sa
GO

-- spAccount_GetPTPByAccNumberAndDueDate

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAccount_GetPTPByAccNumberAndDueDate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spAccount_GetPTPByAccNumberAndDueDate]
GO

CREATE PROCEDURE [dbo].[spAccount_GetPTPByAccNumberAndDueDate]
	@AccNumber INT,
    @DueDate DATETIME
AS
    SELECT PTP_Amount, PTP_Date, Date_Created
    FROM Account_PTP
    WHERE AccNumber = @AccNumber AND PTP_Date > @DueDate
    ORDER BY Account_PTP_ID DESC    
GO

GRANT EXECUTE
ON spAccount_GetPTPByAccNumberAndDueDate
TO webcentral_public_sa
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

-- spACT_GetActivationWorkLog

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spACT_GetActivationWorkLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
  drop procedure [dbo].[spACT_GetActivationWorkLog]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE Procedure [dbo].[spACT_GetActivationWorkLog] @Accnumber int 
AS

select awl.Activation_Work_Log_ID, awa.Description, awa.Activity_Type, awl.UserID, awl.Assigned_By, awl.ILEC, awl.Work_Start, awl.Work_Finish
from Activation_Work_Log as awl join Activation_Work_Activity as awa on awl.Activation_Work_Activity_ID = awa.Activation_Work_Activity_ID
join Activation_Work_Status as aws on aws.Activation_Work_Status_ID = awl.Activation_Work_Status_ID
where accnumber = @Accnumber

union

select awl.Activation_Work_Log_ID, awa.Description, awa.Activity_Type, awl.UserID, awl.Assigned_By, awl.ILEC, awl.Work_Start, awl.Work_Finish
from Activation_Work_Log_archive as awl join Activation_Work_Activity as awa on awl.Activation_Work_Activity_ID = awa.Activation_Work_Activity_ID
join Activation_Work_Status as aws on aws.Activation_Work_Status_ID = awl.Activation_Work_Status_ID
where accnumber = @Accnumber
order by work_start desc
GO

GRANT EXECUTE
ON spACT_GetActivationWorkLog
TO webcentral_public_sa
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

