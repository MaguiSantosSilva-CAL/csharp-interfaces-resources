use maguiss
go

DECLARE @username nvarchar(50) = 'Magui'
DECLARE @password varbinary (250) = CAST(CONVERT(nvarchar,'letmein') as varbinary(250))
SELECT @username, @password

DECLARE @sessionID uniqueidentifier = (select polygon.fn_getCreatedSession(@username));
DECLARE @metadata varchar(500)= CONCAT(Convert(nvarchar,@@SERVERNAME),' ',Convert(nvarchar,HOST_NAME()));

exec polygon.usp_userLoginSuccessful @username='Magui', @password=@password

	  		UPDATE polygon.users
		SET dateLastLogin	=	CURRENT_TIMESTAMP,
			loginSuccessful =	CAST(ISNULL((SELECT 1 from polygon.users where username = @Username and pwd = @password),0) as tinyint),
			sessionId  = @sessionID 
			,metadata  = @metadata
		WHERE username = @username

		select * from polygon.sessionActivity
		select * from polygon.users where username=@username
		select top (5) * from polygon.sessionID order by id desc
		

