USE MAGUISS
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
-- Author:		magui
-- Create date: 07/10/2021
-- Description:	creates new user session
-- =============================================
IF (SELECT type from sys.objects where name = 'usp_SessionCreate' and schema_id = schema_id('polygon')) = 'P'
BEGIN
	DROP PROCEDURE polygon.usp_SessionCreate 
END
GO

CREATE PROCEDURE polygon.usp_SessionCreate 

	@username  nvarchar(50) = null,
	@sessionID uniqueidentifier OUTPUT 
	  
AS

	
	SET NOCOUNT ON;

	-- polygon.sessionID
	INSERT INTO polygon.sessionID(username) VALUES (@username);
	
	SELECT @sessionID = polygon.fn_getCreatedSession(@username) 

	RETURN
	

GO


