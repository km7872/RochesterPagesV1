CREATE PROCEDURE [dbo].[createPrimaryKey]
	@tableName varchar(50),
	@pkId varchar(12) output
AS

DECLARE @locCode varchar(4)
DECLARE @currentCounter int =0
	SELECT TOP 1 @locCode= Site_LocCode FROM SiteInfo
	SELECT @currentCounter= Value From Counter where TableName = @tableName

	SET @currentCounter = @currentCounter + 1
	
	UPDATE Counter
	Set Value= @currentCounter
	WHERE TableName = @tableName
	
	SELECT @pkId =  @locCode+RIGHT('00000000'+ CAST(@currentCounter AS VARCHAR(4)),8)

RETURN
