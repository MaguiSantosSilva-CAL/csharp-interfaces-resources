USE MAGUISS
GO

	select * from polygon.users	
	select * from polygon.sessionID
	select * from polygon.sessionActivity;

DECLARE @username nvarchar(50) 
DECLARE @password varbinary(250)

DECLARE @iterations int = 100
DECLARE @i int = 0

DECLARE @errorcount int = 0

BEGIN TRY

	WHILE @i < @iterations
	BEGIN
										
		with randomPassword (passwordList) as
		(
			select pwd from polygon.users
	
			union all

			select cast(rand() as varbinary(64))
	
		)

		select @password = (select top (1) * from randomPassword order by NEWID())
		select @username = (select top (1) username from polygon.users order by newID())

		select @username as Username, CONVERT(nvarchar(100),@password) as password

		BEGIN TRY
		 BEGIN TRANSACTION
		 exec polygon.usp_attemptLogin @username=@username, @password=@password, @verbose=1, @debug=1

		 COMMIT
		END TRY
		BEGIN CATCH
			ROLLBACK
		END CATCH

		SET @i += 1

	END

END TRY
BEGIN CATCH
	
	SET @errorcount += 1

END CATCH

IF @errorcount > 0
BEGIN
	DECLARE @ERROR varchar(500) = ERROR_MESSAGE()
	SELECT @errorcount as errorCount, @ERROR as ErrorMessage
END
