CREATE PROCEDURE [dbo].[SearchDirectory]
	@lookupName varchar(50)
AS
	SET @lookupName= '%'+ @lookupName+'%'
	select Directory.D_ID, Directory.D_Name, Directory.D_Address, Directory.D_Number 
	from Directory where D_Name like @lookupName or D_Address like @lookupName

	union

	select Directory.D_ID, Directory.D_Name, Directory.D_Address, Directory.D_Number 
	from Tags inner join Directory_Tag_Mapping on DTM_Tag_ID= Tag_ID 
	inner join Directory on Directory.D_ID = Directory_Tag_Mapping.DTM_Directory_ID
	where Tag_Name like @lookupName
	order by 2

