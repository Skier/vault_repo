SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotificationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NotificationType](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [TypeName] [varchar](50) NOT NULL,
    [SendToAdmin] [bit] NOT NULL,
    [SendToPartner] [bit] NOT NULL,
    [SendToPartnerUsers] [bit] NOT NULL,
    [SendToStaff] [bit] NOT NULL,
    [SendToSalesRep] [bit] NOT NULL,
    [SendToAccountant] [bit] NOT NULL,
 CONSTRAINT [PK_NotificationType] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LeadStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LeadStatus](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [StatusName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LeadStatus] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingPhone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrackingPhone](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [PhoneNumber] [varchar](50) NOT NULL,
    [FriendlyNumber] [varchar](50) NULL,
    [Description] [varchar](250) NULL,
    [RedirectPhoneNumber] [varchar](50) NOT NULL,
    [TwilioNumberId] [varchar](50) NULL,
    [DateCreated] [datetime] NULL,
    [BusinessPartnerId] [int] NULL,
    [IsSuspended] [bit] NOT NULL,
    [IsRemoved] [bit] NOT NULL,
    [CallerIdLookup] [bit] NOT NULL,
    [TranscribeCalls] [bit] NOT NULL,
    [IsTollFree] [bit] NOT NULL,
 CONSTRAINT [PK_TrackingPhone] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TransactionType](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [TypeName] [varchar](50) NOT NULL,
    [BaseCost] [money] NOT NULL,
 CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PhoneBlackList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PhoneBlackList](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [PhoneNumber] [varchar](50) NOT NULL,
    [Description] [varchar](250) NULL,
    [PhoneDigits] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PhoneBlackList] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PartnerPhoneNumber]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PartnerPhoneNumber](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [BusinessPartnerId] [int] NOT NULL,
    [PhoneNumber] [varchar](50) NOT NULL,
    [Description] [varchar](250) NULL,
    [PhoneDigits] [varchar](10) NOT NULL,
 CONSTRAINT [PK_PartnerPhoneNumber] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campaign]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Campaign](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CampaignName] [varchar](250) NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [DateStart] [datetime] NOT NULL,
    [DateEnd] [datetime] NULL,
    [BusinessPartnerId] [int] NULL,
    [UserId] [int] NOT NULL,
 CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lead]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Lead](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [LeadStatusId] [int] NOT NULL,
    [CampaignId] [int] NULL,
    [BusinessPartnerId] [int] NULL,
    [DateCreated] [datetime] NOT NULL,
    [FirstName] [varchar](150) NULL,
    [LastName] [varchar](150) NULL,
    [Phone] [varchar](50) NULL,
    [Address] [varchar](250) NULL,
    [CustomerNotes] [varchar](1024) NULL,
    [SourceId] [int] NOT NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [Email] [varchar](150) NULL,
    [FirstName] [varchar](50) NULL,
    [LastName] [varchar](50) NULL,
    [ScreenName] [varchar](100) NULL,
    [Phone] [varchar](50) NULL,
    [Address] [varchar](250) NULL,
    [QbUserId] [varchar](50) NULL,
    [QbRoleName] [varchar](50) NULL,
    [BusinessPartnerId] [int] NULL,
    [DateLastAccess] [datetime] NULL,
    [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Source]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Source](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [PhoneCallId] [int] NULL,
    [PhoneSmsId] [int] NULL,
    [WebFormId] [int] NULL,
    [UserId] [int] NULL,
 CONSTRAINT [PK_Source] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessPartner]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessPartner](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [PartnerName] [varchar](50) NULL,
    [Email] [varchar](150) NULL,
    [Phone] [varchar](50) NULL,
    [PhoneDigits] [varchar](50) NULL,
    [SalesRepId] [int] NULL,
    [IsRemoved] [bit] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [Address] [varchar](500) NULL,
    [IsExcludedFromReports] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessPartner] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivityLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ActivityLog](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [UserId] [int] NULL,
    [ActivityNotes] [varchar](1024) NOT NULL,
 CONSTRAINT [PK_UserActivityLog] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notification]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Notification](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [NotificationTypeId] [int] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [DateProcessed] [datetime] NULL,
    [IsProcessed] [bit] NOT NULL,
    [FromEmail] [varchar](150) NOT NULL,
    [ToEmail] [varchar](150) NOT NULL,
    [Message] [varchar](5000) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PhoneCall]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PhoneCall](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [TrackingPhoneId] [int] NOT NULL,
    [TrackingPhoneNumber] [varchar](50) NULL,
    [CallDuration] [numeric](18, 3) NULL,
    [RecordingUrl] [varchar](250) NULL,
    [DateCreated] [datetime] NOT NULL,
    [Status] [varchar](50) NULL,
    [CampaignId] [int] NULL,
    [CallerName] [varchar](150) NULL,
    [FromPhone] [varchar](50) NULL,
    [FromCity] [varchar](150) NULL,
    [FromState] [varchar](150) NULL,
    [FromZip] [varchar](50) NULL,
    [FromCountry] [varchar](150) NULL,
    [TwilioCallId] [varchar](50) NULL,
    [TrackingPhoneRotationId] [int] NULL,
    [TwilioRecordingUrl] [varchar](250) NULL,
    [IsProcessed] [bit] NOT NULL,
    [PhoneBlackListId] [int] NULL,
    [Notes] [varchar](50) NULL,
 CONSTRAINT [PK_PhoneCall] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PhoneSms]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PhoneSms](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [TrackingPhoneId] [int] NOT NULL,
    [TrackingPhoneNumber] [varchar](50) NULL,
    [Message] [varchar](1024) NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [CampaignId] [int] NULL,
    [FromPhone] [varchar](50) NULL,
    [Status] [varchar](50) NULL,
    [TwilioSmsId] [varchar](50) NULL,
    [TrackingPhoneRotationId] [int] NULL,
 CONSTRAINT [PK_PhoneSms] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transaction](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [TransactionTypeId] [int] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [SourceId] [int] NULL,
    [Description] [varchar](500) NULL,
    [Cost] [money] NOT NULL,
    [Quantity] [numeric](18, 3) NOT NULL,
    [Amount] [money] NOT NULL,
    [CurrentBalance] [money] NOT NULL,
    [QbmsTransactionId] [int] NULL,
    [CampaignId] [int] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingPhoneRotation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrackingPhoneRotation](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CampaignId] [int] NOT NULL,
    [TrackingPhoneId] [int] NOT NULL,
    [ShowedPhoneNumber] [varchar](50) NOT NULL,
    [TimeRotation] [datetime] NOT NULL,
    [SessionUid] [varchar](50) NOT NULL,
    [UserHosAddress] [varchar](150) NOT NULL,
    [RotationPageUri] [varchar](1024) NOT NULL,
    [ReferralUri] [varchar](1024) NOT NULL,
 CONSTRAINT [PK_TrackingPhoneRotation] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompaignTrackingPhone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CompaignTrackingPhone](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CampaignId] [int] NOT NULL,
    [TrackingPhoneId] [int] NOT NULL,
    [DateAssigned] [datetime] NOT NULL,
    [DateReleased] [datetime] NULL,
 CONSTRAINT [PK_CompaignTrackingPhone_1] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QbInvoice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QbInvoice](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [LeadId] [int] NOT NULL,
    [QbInvoiceId] [varchar](50) NOT NULL,
 CONSTRAINT [PK_QbInvoice] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebForm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebForm](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CampagnId] [int] NULL,
    [DateCreated] [datetime] NOT NULL,
    [FirstName] [varchar](150) NOT NULL,
    [LastName] [varchar](150) NOT NULL,
    [Phone] [varchar](150) NOT NULL,
    [Message] [varchar](150) NOT NULL,
    [WebPageUri] [varchar](1024) NOT NULL,
 CONSTRAINT [PK_WebForm] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PartnerPhoneNumber_BusinessPartner]') AND parent_object_id = OBJECT_ID(N'[dbo].[PartnerPhoneNumber]'))
ALTER TABLE [dbo].[PartnerPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_PartnerPhoneNumber_BusinessPartner] FOREIGN KEY([BusinessPartnerId])
REFERENCES [dbo].[BusinessPartner] ([Id])
GO
ALTER TABLE [dbo].[PartnerPhoneNumber] CHECK CONSTRAINT [FK_PartnerPhoneNumber_BusinessPartner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Campaign_BusinessPartner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Campaign]'))
ALTER TABLE [dbo].[Campaign]  WITH CHECK ADD  CONSTRAINT [FK_Campaign_BusinessPartner] FOREIGN KEY([BusinessPartnerId])
REFERENCES [dbo].[BusinessPartner] ([Id])
GO
ALTER TABLE [dbo].[Campaign] CHECK CONSTRAINT [FK_Campaign_BusinessPartner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Campaign_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Campaign]'))
ALTER TABLE [dbo].[Campaign]  WITH CHECK ADD  CONSTRAINT [FK_Campaign_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Campaign] CHECK CONSTRAINT [FK_Campaign_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Lead_BusinessPartner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Lead]'))
ALTER TABLE [dbo].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_BusinessPartner] FOREIGN KEY([BusinessPartnerId])
REFERENCES [dbo].[BusinessPartner] ([Id])
GO
ALTER TABLE [dbo].[Lead] CHECK CONSTRAINT [FK_Lead_BusinessPartner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Lead_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[Lead]'))
ALTER TABLE [dbo].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[Lead] CHECK CONSTRAINT [FK_Lead_Campaign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Lead_LeadStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Lead]'))
ALTER TABLE [dbo].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_LeadStatus] FOREIGN KEY([LeadStatusId])
REFERENCES [dbo].[LeadStatus] ([Id])
GO
ALTER TABLE [dbo].[Lead] CHECK CONSTRAINT [FK_Lead_LeadStatus]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Lead_Source]') AND parent_object_id = OBJECT_ID(N'[dbo].[Lead]'))
ALTER TABLE [dbo].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_Source] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Source] ([Id])
GO
ALTER TABLE [dbo].[Lead] CHECK CONSTRAINT [FK_Lead_Source]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_BusinessPartner]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_BusinessPartner] FOREIGN KEY([BusinessPartnerId])
REFERENCES [dbo].[BusinessPartner] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_BusinessPartner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Source_PhoneCall]') AND parent_object_id = OBJECT_ID(N'[dbo].[Source]'))
ALTER TABLE [dbo].[Source]  WITH CHECK ADD  CONSTRAINT [FK_Source_PhoneCall] FOREIGN KEY([PhoneCallId])
REFERENCES [dbo].[PhoneCall] ([Id])
GO
ALTER TABLE [dbo].[Source] CHECK CONSTRAINT [FK_Source_PhoneCall]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Source_PhoneSms]') AND parent_object_id = OBJECT_ID(N'[dbo].[Source]'))
ALTER TABLE [dbo].[Source]  WITH CHECK ADD  CONSTRAINT [FK_Source_PhoneSms] FOREIGN KEY([PhoneSmsId])
REFERENCES [dbo].[PhoneSms] ([Id])
GO
ALTER TABLE [dbo].[Source] CHECK CONSTRAINT [FK_Source_PhoneSms]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Source_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Source]'))
ALTER TABLE [dbo].[Source]  WITH CHECK ADD  CONSTRAINT [FK_Source_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Source] CHECK CONSTRAINT [FK_Source_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Source_WebForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Source]'))
ALTER TABLE [dbo].[Source]  WITH CHECK ADD  CONSTRAINT [FK_Source_WebForm] FOREIGN KEY([WebFormId])
REFERENCES [dbo].[WebForm] ([Id])
GO
ALTER TABLE [dbo].[Source] CHECK CONSTRAINT [FK_Source_WebForm]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessPartner_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessPartner]'))
ALTER TABLE [dbo].[BusinessPartner]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPartner_User] FOREIGN KEY([SalesRepId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BusinessPartner] CHECK CONSTRAINT [FK_BusinessPartner_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ActivityLog_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ActivityLog]'))
ALTER TABLE [dbo].[ActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_ActivityLog_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ActivityLog] CHECK CONSTRAINT [FK_ActivityLog_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Notification_NotificationType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Notification]'))
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_NotificationType] FOREIGN KEY([NotificationTypeId])
REFERENCES [dbo].[NotificationType] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_NotificationType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneCall_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneCall]'))
ALTER TABLE [dbo].[PhoneCall]  WITH CHECK ADD  CONSTRAINT [FK_PhoneCall_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[PhoneCall] CHECK CONSTRAINT [FK_PhoneCall_Campaign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneCall_PhoneBlackList]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneCall]'))
ALTER TABLE [dbo].[PhoneCall]  WITH CHECK ADD  CONSTRAINT [FK_PhoneCall_PhoneBlackList] FOREIGN KEY([PhoneBlackListId])
REFERENCES [dbo].[PhoneBlackList] ([Id])
GO
ALTER TABLE [dbo].[PhoneCall] CHECK CONSTRAINT [FK_PhoneCall_PhoneBlackList]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneCall_TrackingPhone]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneCall]'))
ALTER TABLE [dbo].[PhoneCall]  WITH CHECK ADD  CONSTRAINT [FK_PhoneCall_TrackingPhone] FOREIGN KEY([TrackingPhoneId])
REFERENCES [dbo].[TrackingPhone] ([Id])
GO
ALTER TABLE [dbo].[PhoneCall] CHECK CONSTRAINT [FK_PhoneCall_TrackingPhone]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneCall_TrackingPhoneRotation]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneCall]'))
ALTER TABLE [dbo].[PhoneCall]  WITH CHECK ADD  CONSTRAINT [FK_PhoneCall_TrackingPhoneRotation] FOREIGN KEY([TrackingPhoneRotationId])
REFERENCES [dbo].[TrackingPhoneRotation] ([Id])
GO
ALTER TABLE [dbo].[PhoneCall] CHECK CONSTRAINT [FK_PhoneCall_TrackingPhoneRotation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneSms_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneSms]'))
ALTER TABLE [dbo].[PhoneSms]  WITH CHECK ADD  CONSTRAINT [FK_PhoneSms_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[PhoneSms] CHECK CONSTRAINT [FK_PhoneSms_Campaign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneSms_TrackingPhone]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneSms]'))
ALTER TABLE [dbo].[PhoneSms]  WITH CHECK ADD  CONSTRAINT [FK_PhoneSms_TrackingPhone] FOREIGN KEY([TrackingPhoneId])
REFERENCES [dbo].[TrackingPhone] ([Id])
GO
ALTER TABLE [dbo].[PhoneSms] CHECK CONSTRAINT [FK_PhoneSms_TrackingPhone]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneSms_TrackingPhoneRotation]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneSms]'))
ALTER TABLE [dbo].[PhoneSms]  WITH CHECK ADD  CONSTRAINT [FK_PhoneSms_TrackingPhoneRotation] FOREIGN KEY([TrackingPhoneRotationId])
REFERENCES [dbo].[TrackingPhoneRotation] ([Id])
GO
ALTER TABLE [dbo].[PhoneSms] CHECK CONSTRAINT [FK_PhoneSms_TrackingPhoneRotation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transaction_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Campaign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transaction_Source]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Source] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Source] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Source]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transaction_TransactionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionType] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_TransactionType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrackingPhoneRotation_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrackingPhoneRotation]'))
ALTER TABLE [dbo].[TrackingPhoneRotation]  WITH CHECK ADD  CONSTRAINT [FK_TrackingPhoneRotation_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[TrackingPhoneRotation] CHECK CONSTRAINT [FK_TrackingPhoneRotation_Campaign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrackingPhoneRotation_TrackingPhone]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrackingPhoneRotation]'))
ALTER TABLE [dbo].[TrackingPhoneRotation]  WITH CHECK ADD  CONSTRAINT [FK_TrackingPhoneRotation_TrackingPhone] FOREIGN KEY([TrackingPhoneId])
REFERENCES [dbo].[TrackingPhone] ([Id])
GO
ALTER TABLE [dbo].[TrackingPhoneRotation] CHECK CONSTRAINT [FK_TrackingPhoneRotation_TrackingPhone]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompaignTrackingPhone_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompaignTrackingPhone]'))
ALTER TABLE [dbo].[CompaignTrackingPhone]  WITH CHECK ADD  CONSTRAINT [FK_CompaignTrackingPhone_Campaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[CompaignTrackingPhone] CHECK CONSTRAINT [FK_CompaignTrackingPhone_Campaign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompaignTrackingPhone_TrackingPhone]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompaignTrackingPhone]'))
ALTER TABLE [dbo].[CompaignTrackingPhone]  WITH CHECK ADD  CONSTRAINT [FK_CompaignTrackingPhone_TrackingPhone] FOREIGN KEY([TrackingPhoneId])
REFERENCES [dbo].[TrackingPhone] ([Id])
GO
ALTER TABLE [dbo].[CompaignTrackingPhone] CHECK CONSTRAINT [FK_CompaignTrackingPhone_TrackingPhone]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QbInvoice_Lead]') AND parent_object_id = OBJECT_ID(N'[dbo].[QbInvoice]'))
ALTER TABLE [dbo].[QbInvoice]  WITH CHECK ADD  CONSTRAINT [FK_QbInvoice_Lead] FOREIGN KEY([LeadId])
REFERENCES [dbo].[Lead] ([Id])
GO
ALTER TABLE [dbo].[QbInvoice] CHECK CONSTRAINT [FK_QbInvoice_Lead]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WebForm_Campaign]') AND parent_object_id = OBJECT_ID(N'[dbo].[WebForm]'))
ALTER TABLE [dbo].[WebForm]  WITH CHECK ADD  CONSTRAINT [FK_WebForm_Campaign] FOREIGN KEY([CampagnId])
REFERENCES [dbo].[Campaign] ([Id])
GO
ALTER TABLE [dbo].[WebForm] CHECK CONSTRAINT [FK_WebForm_Campaign]
