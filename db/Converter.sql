USE [Site]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function [dbo].[strtobinary](@ip as varchar(15)) returns binary(4)
as
begin
declare @b as binary(4)
select @b = cast( cast( parsename(@ip, 4) as integer) as binary(1))
		  + cast( cast( parsename(@ip, 3) as integer) as binary(1))
		  + cast( cast( parsename(@ip, 2) as integer) as binary(1))
		  + cast( cast( parsename(@ip, 1) as integer) as binary(1))

return @b
end
