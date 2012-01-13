
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Split]
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 05/11/2011 10:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N' CREATE FUNCTION [dbo].[Split]
    (
        @String nvarchar(4000), 
        @Separator char(1)
    )
RETURNS TABLE
AS
RETURN (
    WITH Variables(p, a, b) AS (
        Select 
			1, 1, CharIndex(@Separator, @String)
        Union all
        Select
            p + 1, 
            b + 1, 
            CharIndex(@Separator, @String, b + 1)
        From Variables
        Where b > 0
    )
    Select
        p - 1 As ZeroBasedOccurance,
        SubString(
            @String, 
            a, 
            Case When b > 0 Then b - a Else 4000 End) 
        AS Word
    From Variables
  )
' 
END
GO
