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
CREATE FUNCTION polygon.getNextSessionID 
(			 
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @nextSessionID int

	-- Add the T-SQL statements to compute the return value here
	DECLARE @maxSessionID int = (select isnull(max(sessionID),0) from polygon.SessionID)

	SET @nextSessionID = @maxSessionID+1

	-- Return the result of the function
	RETURN @nextSessionID

END
GO

