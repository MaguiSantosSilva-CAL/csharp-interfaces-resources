USE maguiss
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		maguiss
-- Create date: 24/09/2021
-- Description:	to run on user's successful login
-- =============================================
IF (SELECT type from sys.objects where name = 'usp_attemptLogin' and schema_id = schema_id('polygon')) = 'P'
BEGIN
	DROP PROCEDURE polygon.usp_attemptLogin
END
GO

CREATE PROCEDURE polygon.usp_attemptLogin 
	-- Add the parameters for the stored procedure here
	@username	nvarchar(250) = null, 
	@password	nvarchar(250) = null,
	@verbose	tinyint = 0,
	@debug		tinyint =0

AS
BEGIN
	SET NOCOUNT OFF;

	SET @username = TRIM(@username)
	SET @password = polygon.fn_stringToPassword(@password);	  																 
	DECLARE @errorCount int = 0;
	DECLARE @eventType nvarchar(50) = (select id from polygon.accountActivityTypes where eventType = 'login');

	DECLARE @usernameAndPasswordCorrect tinyint = CAST(ISNULL((SELECT 1 
													from polygon.users 
													where	username = @Username 
													and		pwd = @password)
													,0) as tinyint)
	
	DECLARE @activeSessionExists uniqueidentifier = (select polygon.fn_getActiveSessionID(@username))
	DECLARE @current_timestamp datetime		= CURRENT_TIMESTAMP				
	DECLARE @metadata		nvarchar(100)	= CONCAT(
												Convert(nvarchar,@@SERVERNAME)
												,' '
												,Convert(nvarchar,HOST_NAME()));
	
	DECLARE @sessionIDOUT uniqueidentifier;		-- Out parameter
	
	UPDATE	polygon.users
	 
	 SET	dateLastLogin	=	@current_timestamp 
	 	   ,userLoggedIn	=	@usernameAndPasswordCorrect
	 	   ,metadata		=	@metadata
	 
	 WHERE   LOWER(username)	=	LOWER(@username)
	 	
	 IF @usernameAndPasswordCorrect != 1 GOTO EOF
	 

	 IF @activeSessionExists is null 
	 	BEGIN /* If there is no active session */
		
		EXEC polygon.usp_SessionCreate @username, @sessionIDOUT OUTPUT;
		
			BEGIN TRY
				
				UPDATE	polygon.users
				SET		sessionId	=	@sessionIDOUT 
				where	username	=	@username
					
				INSERT INTO polygon.sessionActivity(sessionID
												   ,created
												   ,eventType) 
				VALUES (@sessionIDOUT
						,@current_timestamp 
						,@eventType);		
			
				IF @verbose != 0 
					SELECT information from polygon.users where username = @username

			END TRY

			BEGIN CATCH 
			
					SET @errorCount += 1
					SELECT 'an error occurred'

			END CATCH
		
		END /* no active session */
		ELSE
		BEGIN
		
			UPDATE	polygon.sessionActivity
				   SET	eventInformation				 = 'User already logged in'
					   ,lastAccountActivityDuringSession = @current_timestamp 
					   ,eventType						 = @eventType
				 WHERE	sessionID = @activeSessionExists;

			IF @verbose != 0 SELECT 'Active session already exists' as loginSuccessful;
		END

	
EOF:	
	IF @debug = 1 
		SELECT	information 
		FROM	polygon.users 
		WHERE	username = @username 
			and pwd		 = @password
					
END
GO

USE MAGUISS
IF (SELECT [type] from sys.objects where name like '%login%' and schema_name([schema_id]) = 'user') = 'SN' drop synonym [user].[login]

create synonym [user].[login] for polygon.usp_attemptLogin
--GRANT EXECUTE ON  polygon.usp_attemptLogin to polygonSys

exec [user].login
exec polygon.usp_attemptLogin
GO
