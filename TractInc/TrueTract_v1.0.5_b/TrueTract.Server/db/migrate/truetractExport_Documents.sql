
begin tran

declare @DocId int, @IsPublic int, @DocTypeId int, @Volume varchar(50), @Page varchar(50), 
		@DocumentNo varchar(50), @County int, @State int, 
		@DateFiledYear int, @DateFiledMonth int, @DateFiledDay int, 
	    @DateSignedYear int, @DateSignedMonth int, @DateSignedDay int, @ResearchNote varchar(350), @ImageLink varchar(350)
declare @NewDocId int



declare @TractId int, @NewTractId int

declare document_cursor cursor FORWARD_ONLY for (

	select docTest.DocId, docTest.IsPublic, docTest.DocTypeId, docTest.Volume, docTest.Page, docTest.DocumentNo, docTest.County, docTest.State, 
		docTest.DateFiledYear, docTest.DateFiledMonth, docTest.DateFiledDay, 
	    docTest.DateSignedYear, docTest.DateSignedMonth, docTest.DateSignedDay, docTest.ResearchNote, docTest.ImageLink
	  from tractincTest.dbo.[document] docTest
		left outer join tractincProduction.dbo.[document] docProd on 
			docProd.DocTypeId = docTest.DocTypeId and
			docProd.State = docTest.State and
			docProd.County = docTest.County
	  where docProd.DocId is null
)

open document_cursor
FETCH NEXT FROM document_cursor INTO @DocId, @IsPublic, @DocTypeId, @Volume, @Page, 
		@DocumentNo, @County, @State, 
		@DateFiledYear, @DateFiledMonth, @DateFiledDay, 
	    @DateSignedYear, @DateSignedMonth, @DateSignedDay, @ResearchNote, @ImageLink

WHILE @@FETCH_STATUS = 0
BEGIN

	print 'DocumentId ' + cast(@DocId as varchar)

	-- insert document
	insert into tractincProduction.dbo.document (IsPublic, DocTypeId, Volume, Page, DocumentNo, County, State, 
		DateFiledYear, DateFiledMonth, DateFiledDay, 
	    DateSignedYear, DateSignedMonth, DateSignedDay, ResearchNote, ImageLink)
	values (@IsPublic, @DocTypeId, @Volume, @Page, 
		@DocumentNo, @County, @State, 
		@DateFiledYear, @DateFiledMonth, @DateFiledDay, 
	    @DateSignedYear, @DateSignedMonth, @DateSignedDay, @ResearchNote, @ImageLink)

	set @NewDocId = @@IDENTITY

	-- insert document participants
	insert into tractincProduction.dbo.participant (DocID, AsNamed, FirstName, MiddleName, LastName, IsSeller)
	select @NewDocID, AsNamed, FirstName, MiddleName, LastName, IsSeller
	 from tractincTest.dbo.[participant]
		where docId = @DocId


	-- insert document Tracts and Tracts related records
	declare tract_cursor cursor FORWARD_ONLY for (
		select TractId
		  from tractincTest.dbo.Tract
		 where docId = @DocId
	)

	open tract_cursor
	FETCH NEXT FROM tract_cursor into @TractId

	WHILE @@FETCH_STATUS = 0
	BEGIN

		print 'TractId ' + cast( @TractId as varchar)

		-- insert Tracts
		insert into tractincProduction.dbo.tract (Easting, Northing, RefName, CreatedBy, IsDeleted, DocID, CalledAC, UnitId)
		select Easting, Northing, RefName, prodUser.UserId, IsDeleted, @NewDocID, CalledAC, UnitId
		  from tractincTest.dbo.tract t
			join tractincTest.dbo.[user] testUser on testUser.UserId = t.CreatedBy
			join tractincProduction.dbo.[user] prodUser on prodUser.Login = testUser.Login
		 where TractId = @TractId

		set @NewTractId = @@IDENTITY
		
		print 'NewTractId ' + cast( @NewTractId as varchar)

		-- insert Tract Calls
		INSERT INTO tractincProduction.[dbo].[TractCalls] ([TractId], CallType, CallDBValue, CallOrder, CreatedByMouse)
        SELECT @NewTractId, CallType, CallDBValue, CallOrder, CreatedByMouse
		  FROM tractincTest.dbo.tractcalls where tractId = @TractId

		-- insert Tract Text Objects
		INSERT INTO tractincProduction.[dbo].[TractTextObjects] ([TractId], [Text], [Easting], [Northing], [Rotation])
        SELECT @NewTractId, [Text], Easting, Northing, Rotation
		  FROM tractincTest.dbo.[TractTextObjects] where tractId = @TractId


		FETCH NEXT FROM tract_cursor into @TractId
	END

	CLOSE tract_cursor
	DEALLOCATE tract_cursor

	FETCH NEXT FROM document_cursor INTO @DocId, @IsPublic, @DocTypeId, @Volume, @Page, 
			@DocumentNo, @County, @State, 
			@DateFiledYear, @DateFiledMonth, @DateFiledDay, 
			@DateSignedYear, @DateSignedMonth, @DateSignedDay, @ResearchNote, @ImageLink

END

CLOSE document_cursor
DEALLOCATE document_cursor


-- export drawings from test ot production

-- insert Tracts and Tracts related records
declare drawing_cursor cursor FORWARD_ONLY for (
	select TractId
	  from tractincTest.dbo.Tract
	 where docId is null
)

open drawing_cursor
FETCH NEXT FROM drawing_cursor into @TractId

WHILE @@FETCH_STATUS = 0
BEGIN

	print 'TractId ' + cast( @TractId as varchar)

	-- insert Tracts
	insert into tractincProduction.dbo.tract (Easting, Northing, RefName, CreatedBy, IsDeleted, DocID, CalledAC, UnitId)
	select Easting, Northing, RefName, prodUser.UserId, IsDeleted, null, CalledAC, UnitId
	  from tractincTest.dbo.tract t
		join tractincTest.dbo.[user] testUser on testUser.UserId = t.CreatedBy
		join tractincProduction.dbo.[user] prodUser on prodUser.Login = testUser.Login
	 where TractId = @TractId

	set @NewTractId = @@IDENTITY

	-- insert Tract Calls
	INSERT INTO tractincProduction.[dbo].[TractCalls] ([TractId], CallType, CallDBValue, CallOrder, CreatedByMouse)
    SELECT @NewTractId, CallType, CallDBValue, CallOrder, CreatedByMouse
	  FROM tractincTest.dbo.tractcalls where tractId = @TractId

	-- insert Tract Text Objects
	INSERT INTO tractincProduction.[dbo].[TractTextObjects] ([TractId], [Text], [Easting], [Northing], [Rotation])
    SELECT @NewTractId, [Text], Easting, Northing, Rotation
	  FROM tractincTest.dbo.[TractTextObjects] where tractId = @TractId


	FETCH NEXT FROM drawing_cursor into @TractId
END

CLOSE drawing_cursor
DEALLOCATE drawing_cursor

commit tran

--rollback