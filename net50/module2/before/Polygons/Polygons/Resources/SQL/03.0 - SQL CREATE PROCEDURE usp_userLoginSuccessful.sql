USE maguiss
GO

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		maguiss
-- Create date: 24/09/2021
-- Description:	to run on user's successful login
-- =============================================
IF (SELECT type from sys.objects where name = 'usp_userLoginSuccessful') = 'P'
BEGIN
	DROP PROCEDURE polygon.usp_userLoginSuccessful
END
GO

CREATE PROCEDURE polygon.usp_userLoginSuccessful 
	-- Add the parameters for the stored procedure here
	@username nvarchar(250) = null, 
	@password nvarchar(250) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	SET @password = polygon.fn_stringToPassword(@password)

	UPDATE polygon.users
	SET dateLastLogin	=	CURRENT_TIMESTAMP,
		loginSuccessful =	CAST(ISNULL((SELECT 1 from polygon.users where username = @Username and pwd = @password),0) as tinyint)
	WHERE username = @username

	

	--SELECT ISNULL(information,'No records found') from polygon.users where username = @username and pwd = @password
	SELECT information from polygon.users where username = @username and pwd = @password
END
GO

GRANT EXECUTE ON  polygon.usp_userLoginSuccessful  to polygonSys
GO