
begin tran

declare @UserId int, @Login varchar(50), @FirstName varchar(50), @LastName varchar(50), @PhoneNumber varchar(50), 
		@Password varchar(50), @Email varchar(50), @IsActive bit, @HackingAttempts int, @NewTracts int
declare @NewUserId int

declare user_cursor cursor FORWARD_ONLY for (

	select [UserId], [Login],[FirstName],[LastName],[PhoneNumber],[Password],[Email],[IsActive],[HackingAttempts],[NewTracts]
	  from tractincTest.dbo.[user]
	 where login not in (select login from tractincProduction.dbo.[user])
)

open user_cursor
FETCH NEXT FROM user_cursor into @UserId, @Login, @FirstName, @LastName, @PhoneNumber, @Password, @Email, @IsActive, @HackingAttempts, @NewTracts

WHILE @@FETCH_STATUS = 0
BEGIN

	print 'UserId ' + cast( @UserId as varchar)

	-- insert to production 
	insert into tractincProduction.[dbo].[User]
       ([Login],[FirstName],[LastName],[PhoneNumber],[Password],[Email],[IsActive],[HackingAttempts],[NewTracts])
	values (@Login, @FirstName, @LastName, @PhoneNumber, @Password, @Email, @IsActive, @HackingAttempts, @NewTracts)


	set @NewUserId = ( select MAX([user_id]) + 1 from walt5.db_ddladmin.[user_def] )

	-- walt5 database
	-- insert user to walt5
    insert into walt5.db_ddladmin.[user_def] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_num])
		values (@NewUserId, @Login, @Password, @FirstName, @LastName, @Email, @PhoneNumber)


	-- update ids table
	update walt5.db_ddladmin.[ids] 
       set LastValue = @NewUserId 
	 where ColumnName = 'user_id'


	-- mapoptics5 database
	-- insert user to mapoptix5 - user
	insert into mapoptix5.db_ddladmin.[user_def_mapoptix] (
			[user_id], [profile_id], [user_admin], [user_active]
	)
	values ( @NewUserId, 5, 0, 1 )


	-- insert user to mapoptix5 - profile
	insert into mapoptix5.db_ddladmin.[profile_user_def] (
			[profile_id], [user_id]
	)
	values ( 5, @NewUserId )

	FETCH NEXT FROM user_cursor into @UserId, @Login, @FirstName, @LastName, @PhoneNumber, @Password, @Email, @IsActive, @HackingAttempts, @NewTracts

END

CLOSE user_cursor
DEALLOCATE user_cursor


-- begin user role importing

insert tractincProduction.dbo.[userRole] (userID, RoleId)
select up.userId, 2
    from tractincProduction.dbo.[user] up
		join tractincTest.dbo.[user] ut on ut.login = up.login
   where up.UserId not in (select UserId from tractincProduction.dbo.[userrole])

-- end user role importing


commit tran

--rollback
