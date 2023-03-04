CREATE PROCEDURE [dbo].[insertDirectory]
	@name varchar(50),
	@address varchar(max),
	@number varchar(10)
AS
	declare @pkId varchar(12)

	if not exists(select * From Directory where D_Name = @name
	and D_Address = @address)
	begin
		exec createPrimaryKey 'DIRECTORY', @pkId OUTPUT

		insert into Directory(D_ID, D_Name, D_Address, D_Number) 
		values(@pkId, @name, @address, @number)
	end	