use maguiss
go
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
-- Create date: 23/9/2021
-- Description:	password string to binary
-- =============================================
IF (SELECT type from sys.objects where name = 'fn_stringToPassword') = 'FN'
BEGIN
	DROP FUNCTION polygon.fn_stringToPassword
END
GO

CREATE FUNCTION polygon.fn_stringToPassword 
(
	-- Add the parameters for the function here
	@password nvarchar(100) = null
)
RETURNS varbinary(64)
AS
BEGIN
	-- Return the result of the function
	RETURN convert(varbinary(64),@password)

END
GO

