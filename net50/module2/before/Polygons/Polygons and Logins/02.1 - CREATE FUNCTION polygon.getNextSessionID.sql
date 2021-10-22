use maguiss
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		maguiss
-- Create date: 7/10/2021
-- Description:	Find next SessionID for Polymer
-- =============================================
IF (SELECT type from sys.objects where name = 'getNextSessionID' and schema_id = schema_id('polygon')) = 'FN'
BEGIN
	DROP FUNCTION polygon.getNextSessionID 
END
GO

CREATE FUNCTION polygon.getNextSessionID 
(			 
	@username varchar(50) = null
)
RETURNS int
AS
BEGIN
	
	DECLARE @maxSessionID int 
	
	IF (@username is null)
		SET @maxSessionID = (select isnull(max(id),0) from maguiss.polygon.SessionID)
	ELSE
		SET @maxSessionID = (select sessionID from maguiss.polygon.SessionID where username = @username and id = (select max(id) from maguiss.polygon.sessionID where username = @username));

	SET @nextSessionID = @maxSessionID+1

	-- Return the result of the function
	RETURN @nextSessionID

END
GO

