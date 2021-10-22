USE maguiss
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		magui
-- Create date: 07/10/2021
-- Description:	creates new user session
-- =============================================
IF (SELECT type from sys.objects where name = 'fn_getCreatedSession' and schema_id = schema_id('polygon')) = 'FN'
BEGIN
	DROP FUNCTION polygon.fn_getCreatedSession 
END
GO

CREATE FUNCTION polygon.fn_getCreatedSession
(
	@username  nvarchar(50) = null
)
RETURNS uniqueidentifier
AS
BEGIN

	DECLARE @sessionID uniqueidentifier = (select sessionID 
										   from maguiss.polygon.sessionID 
										   where id = (select max(id) 
													   from maguiss.polygon.sessionID 
													   where username = @username
										   			   )
										   	);
 
	RETURN @sessionID;

END
GO

