USE maguiss
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		magui
-- Create date: 07/10/2021
-- Description:	checks for existing user activity within past 30 mins
-- =============================================
IF (SELECT type from sys.objects where name = 'fn_getActiveSessionID' and schema_id = schema_id('polygon')) = 'FN'
BEGIN
	DROP FUNCTION polygon.fn_getActiveSessionID
END
GO

CREATE FUNCTION polygon.fn_getActiveSessionID
(
	@username  nvarchar(50) = null
)
RETURNS uniqueidentifier
AS
BEGIN

	DECLARE @activeSessionID uniqueidentifier = (select activeSession
													 from polygon.activeSessionsByUser 
													 where activeInLast30Mins = 1
													 and username = @username);
	
	-- TODO: User should be able to logout. Otherwise, logged out event will remain active for 30 minutes.

	RETURN @activeSessionID;

END
GO

