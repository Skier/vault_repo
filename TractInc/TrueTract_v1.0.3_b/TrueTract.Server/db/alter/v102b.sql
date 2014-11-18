alter table dbo.[User] add
    DefaultSite varchar(250) not null constraint DF_User_DefaultSite default 'Barnett Shale'
