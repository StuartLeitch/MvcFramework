/****** Object:  StoredProcedure [dbo].[FindUsersInRole]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindUsersInRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FindUsersInRole]
GO
/****** Object:  StoredProcedure [dbo].[GetRolesForUser]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRolesForUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRolesForUser]
GO
/****** Object:  StoredProcedure [dbo].[IsUserInRole]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsUserInRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[IsUserInRole]
GO
/****** Object:  StoredProcedure [dbo].[RemoveUsersFromRoles]    Script Date: 05/09/2011 10:55:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveUsersFromRoles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemoveUsersFromRoles]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteUser]
GO
/****** Object:  StoredProcedure [dbo].[GetUsersInRole]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUsersInRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUsersInRole]
GO
/****** Object:  StoredProcedure [dbo].[IsRoleExists]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsRoleExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[IsRoleExists]
GO
/****** Object:  StoredProcedure [dbo].[FindUsersByEmail]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindUsersByEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FindUsersByEmail]
GO
/****** Object:  StoredProcedure [dbo].[FindUsersByName]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindUsersByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FindUsersByName]
GO
/****** Object:  StoredProcedure [dbo].[SetUserLoginDate]    Script Date: 05/09/2011 10:55:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUserLoginDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetUserLoginDate]
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateUser]
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 10/20/2011 13:37:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateRole]
GO
/****** Object:  StoredProcedure [dbo].[UnlockUser]    Script Date: 05/09/2011 10:55:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UnlockUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UnlockUser]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 05/09/2011 10:55:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateUser]
GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 05/09/2011 10:55:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResetPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ResetPassword]
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUser]
GO
/****** Object:  StoredProcedure [dbo].[GetUserByEmail]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserByEmail]
GO
/****** Object:  StoredProcedure [dbo].[GetUserByProviderKey]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByProviderKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserByProviderKey]
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUsername]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByUsername]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserByUsername]
GO
/****** Object:  StoredProcedure [dbo].[GetUserNameByEmail]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserNameByEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserNameByEmail]
GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllRoles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllRoles]
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllUsers]
GO
/****** Object:  StoredProcedure [dbo].[GetNumberOfUsersOnline]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNumberOfUsersOnline]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetNumberOfUsersOnline]
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChangePassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ChangePassword]
GO
/****** Object:  StoredProcedure [dbo].[ChangePasswordQuestionAndAnswer]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChangePasswordQuestionAndAnswer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ChangePasswordQuestionAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[CreateApplication]    Script Date: 05/09/2011 10:55:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateApplication]
GO
/****** Object:  StoredProcedure [dbo].[CreateApplication]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateApplication]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Create new User
-- =============================================
CREATE PROCEDURE [dbo].[CreateApplication]
    @ApplicationName      nvarchar(100),
    @ApplicationID        int OUTPUT
AS
BEGIN
    SELECT  @ApplicationID = ApplicationID FROM Applications WHERE Applications.Name = @ApplicationName

    IF(@ApplicationID IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        INSERT  Applications(Name)
        VALUES  (@ApplicationName)
        
        SET @ApplicationID = SCOPE_IDENTITY()

        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[ChangePasswordQuestionAndAnswer]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChangePasswordQuestionAndAnswer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(100),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId int
    SELECT  @UserId = Users.UserID
    FROM    Users Join Applications On Users.ApplicationID = Applications.ApplicationID
    WHERE   Applications.Name = @ApplicationName And
            Users.Username = @UserName
            
    IF (@UserId IS NULL) BEGIN
        SELECT(1)
    END

    UPDATE Users
    SET    Users.PasswordQuestion = @NewPasswordQuestion, Users.PasswordAnswer = @NewPasswordAnswer
    WHERE  Users.UserID = @UserId
    SELECT(0)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChangePassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Change Password
-- =============================================
CREATE PROCEDURE [dbo].[ChangePassword] 
	@ApplicationName nvarchar(100), 
	@Username nvarchar(256), 
	@Password nvarchar(128),
	@PasswordSalt nvarchar(128),
	@Date Datetime
AS
BEGIN
	Declare @UserID int
	
	Select @UserID = Users.UserID From Users JOIN Applications ON Users.ApplicationID = Applications.ApplicationID
	Where Username = @Username
	
	If (@UserID IS NULL) Begin
		SELECT (1)
	End
	
	UPDATE Users SET Password = @Password, PasswordSalt = @PasswordSalt WHERE Username = @Username And 
		ApplicationID = (SELECT Applications.ApplicationID FROM Applications Where Applications.Name = @ApplicationName)
	
	SELECT (0)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetNumberOfUsersOnline]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNumberOfUsersOnline]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Get the number of Online users
-- =============================================
CREATE PROCEDURE [dbo].[GetNumberOfUsersOnline]
	@ApplicationName nvarchar(100), 
	@MinutesSinceLastInActive int,
	@CurrentTimeUtc DateTime
AS
BEGIN
	DECLARE @Total int
	DECLARE @DateActive datetime
	
    SET  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)    
	Select @Total = COUNT(*) FROM Users 
	Where Users.LastActivityDate > @DateActive AND
		  Users.ApplicationID = (Select Applications.ApplicationID From Applications Where Applications.Name = @ApplicationName)
		  
	SELECT @Total
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Find the Users by Email address
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUsers]
	@ApplicationName nvarchar(100), 
	@PageIndex int,
	@PageSize int,
	@TotalRecord int OUTPUT
AS
BEGIN
	Declare @StartIndex int
	Set @StartIndex = @PageIndex * @PageSize

	SELECT @TotalRecord = COUNT(*) FROM Users Where Users.ApplicationID = (Select Applications.ApplicationID From Applications Where Applications.Name = @ApplicationName);

	WITH SearchQuery AS(
		Select Users.*, ROW_NUMBER() OVER (ORDER BY Username) AS RowNumber
		FROM Users Where Users.ApplicationID = (Select Applications.ApplicationID From Applications Where Applications.Name = @ApplicationName)
	)
	
	SELECT * 
	FROM SearchQuery 
	WHERE RowNumber BETWEEN @StartIndex AND @StartIndex + @PageSize;
	
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	List of the roles
-- =============================================
CREATE PROCEDURE [dbo].[GetAllRoles]
    @ApplicationName	nvarchar(100)
AS
BEGIN
	
    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    SELECT Roles.RoleName FROM Roles
    WHERE  Roles.ApplicationID = @ApplicationID
    
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserNameByEmail]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserNameByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/10/2011
-- Description:	Get the user by Email address
-- =============================================
CREATE PROCEDURE [dbo].[GetUserNameByEmail] 
	@ApplicationName nvarchar(100),
	@Email nvarchar(256),
	@RowCount int OUTPUT
AS
BEGIN
	SELECT Username FROM Users
	Where Users.Email = @Email AND
		  Users.ApplicationID = (SELECT Applications.ApplicationID FROM Applications 
								 WHERE Applications.Name = @ApplicationName)
								 
	SET @RowCount = @@ROWCOUNT
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUsername]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByUsername]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Get the user by Username
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByUsername] 
	@Username nvarchar(256), 
	@ApplicationName nvarchar(100)
AS
BEGIN
	SELECT Users.* FROM Users Join Applications ON Users.ApplicationID = Applications.ApplicationID
	WHERE Users.Username = @Username And Applications.Name = @ApplicationName
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByProviderKey]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByProviderKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Get User with update Activity
-- =============================================
Create PROCEDURE [dbo].[GetUserByProviderKey]
	-- Add the parameters for the stored procedure here
	@ApplicationName nvarchar(100), 
	@UserID int,
    @CurrentTimeUtc datetime,
	@UserIsOnline bit
AS
BEGIN
	
    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    DECLARE @UID int
    Select @UID = Users.UserID From Users Where UserID = @UserID And ApplicationID = @ApplicationID
    
    If (@UID IS NULL)
		SELECT 0
	
	If (@UserIsOnline = 1)
		UPDATE Users SET Users.LastActivityDate = @CurrentTimeUtc Where Users.UserID = @UserID
	
	SELECT * From Users Where Users.UserID = @UserID
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByEmail]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/10/2011
-- Description:	Get the user by Email address
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByEmail] 
	@ApplicationName nvarchar(100),
	@Email nvarchar(256),
	@RowCount int OUTPUT
AS
BEGIN
	SELECT * FROM Users
	Where Users.Email = @Email AND
		  Users.ApplicationID = (SELECT Applications.ApplicationID FROM Applications 
								 WHERE Applications.Name = @ApplicationName)
								 
	SET @RowCount = @@ROWCOUNT
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Get User with update Activity
-- =============================================
CREATE PROCEDURE [dbo].[GetUser] 
	-- Add the parameters for the stored procedure here
	@ApplicationName nvarchar(100), 
	@Username nvarchar(256),
    @CurrentTimeUtc datetime,
	@UserIsOnline bit
AS
BEGIN
	
    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    DECLARE @UserID int
    Select @UserID = Users.UserID From Users Where Username = @Username And ApplicationID = @ApplicationID
    
    If (@UserID IS NULL)
		SELECT 0
	
	If (@UserIsOnline = 1)
		UPDATE Users SET Users.LastActivityDate = @CurrentTimeUtc Where Users.UserID = @UserID
	
	SELECT * From Users Where Users.UserID = @UserID
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 05/09/2011 10:55:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/10/2011
-- Description:	Reset user password
-- =============================================
Create PROCEDURE [dbo].[ResetPassword]
    @ApplicationName             nvarchar(100),
    @Username                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserID                                 int
    SET     @UserID = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserID = Users.UserID
    FROM    Users Inner Join Applications On Users.ApplicationID = Applications.ApplicationID
    WHERE   Users.Username = @Username

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = (Case When Users.Status = 2 Then 1 Else 0 End),
           @LastLockoutDate = Users.LastLockoutDate,
           @FailedPasswordAttemptCount = Users.FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = Users.FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = Users.FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = Users.FailedPasswordAnswerAttemptWindowStart
    FROM Users
    WHERE Users.UserID = @UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE Users
    SET    Users.Password = @NewPassword,
           Users.LastPasswordChangedDate = @CurrentTimeUtc,
           Users.PasswordFormat = @PasswordFormat,
           Users.PasswordSalt = @PasswordSalt
    WHERE  Users.UserID = @UserID AND
           (@PasswordAnswer IS NULL OR Users.PasswordAnswer = @PasswordAnswer)

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE Users
        SET Users.Status = (Case When @IsLockedOut = 1 Then 2 Else 0 End), Users.LastLockoutDate = @LastLockoutDate,
            Users.FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            Users.FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            Users.FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            Users.FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE Users.UserID = @UserID

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    SELECT @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    SELECT @ErrorCode

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 05/09/2011 10:55:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/13/2011
-- Description:	Update user information
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser] 
	@ApplicationName nvarchar(100), 
	@UserID int,
	@RequiresUniqueEmail bit,
	@Email nvarchar(256),
	@Comment nvarchar(max),
	@IsApproved bit,
	@LastLoginDate datetime,
	@UpdateName bit,
	@FirstName nvarchar(100),
	@LastName nvarchar(100)
AS
BEGIN
	IF NOT EXISTS (Select * From Users 
				   Where Users.UserID = @UserID And
					     Users.ApplicationID = (SELECT Applications.ApplicationID From Applications
						    					Where Applications.Name = @ApplicationName)) BEGIN
	
		SELECT (1)
	END
	
	IF (@RequiresUniqueEmail = 1) BEGIN
		IF EXISTS (Select * From Users
				   Where Users.UserID <> @UserID And
						 Users.ApplicationID = (SELECT Applications.ApplicationID From Applications
						    					Where Applications.Name = @ApplicationName) And
						 Users.Email = @Email) BEGIN
			SELECT (7)
		END
	END
	
	IF (@UpdateName = 1) BEGIN
		UPDATE Users SET 
			Users.Email = @Email,
			Users.Comment = @Comment,
			Users.IsApproved = @IsApproved,
			Users.LastLoginDate = @LastLoginDate,
			Users.FirstName = @FirstName,
			Users.LastName = @LastName
		Where Users.UserID = @UserID
	END 
	ELSE BEGIN
		UPDATE Users SET 
			Users.Email = @Email,
			Users.Comment = @Comment,
			Users.IsApproved = @IsApproved,
			Users.LastLoginDate = @LastLoginDate
		Where Users.UserID = @UserID
	END
	
	SELECT (0)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UnlockUser]    Script Date: 05/09/2011 10:55:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UnlockUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/10/2011
-- Description:	Unlock the user
-- =============================================
CREATE PROCEDURE [dbo].[UnlockUser] 
	@ApplicationName nvarchar(100), 
	@Username nvarchar(256)
AS
BEGIN

	DECLARE @UserID int
	
	SELECT @UserID = Users.UserID FROM Users
	WHERE Users.Username = @Username AND
		  Users.ApplicationID = (SELECT Applications.ApplicationID FROM Applications
								 WHERE Applications.Name = @ApplicationName)
								 
	IF (@UserID IS NULL)
		SELECT (0)
		
	UPDATE Users SET
		Users.Status = 1
	WHERE Users.UserID = @UserID
	
	SELECT @@ROWCOUNT

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Create new User
-- =============================================
CREATE PROCEDURE [dbo].[CreateUser]
    @ApplicationName                        nvarchar(100),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @IsAnonymous                            bit,
    @TimeZone								int,
    @CreateOn		                        datetime,
    @UniqueEmail                            bit      = 0,
    @PasswordFormat                         int      = 0,
    @UserID                                 int OUTPUT
AS
BEGIN
    DECLARE @ApplicationID int
    DECLARE @Satus tinyint
    DECLARE @LastLockoutDate datetime
    DECLARE @FailedPasswordAttemptCount int
    DECLARE @FailedPasswordAttemptWindowStart  datetime
    DECLARE @FailedPasswordAnswerAttemptCount int
    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    DECLARE @ReturnValue   int
    DECLARE @ErrorCode     int
    DECLARE @TranStarted   bit
    
    SET @Satus = 1
    SET @LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )
    SET @FailedPasswordAttemptCount = 0
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
    SET @FailedPasswordAnswerAttemptCount = 0
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
    SET @ReturnValue = 0
    SET @ErrorCode = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SELECT  @UserID = Users.UserID FROM Users WHERE Users.Username = @UserName AND Users.ApplicationID = @ApplicationID
    IF ( @UserID IS NOT NULL )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT * FROM  Users WHERE Users.ApplicationID = @ApplicationID AND Email = @Email))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    INSERT INTO Users
                ( ApplicationID,
                  Username,
                  Password,
                  PasswordSalt,
                  Email,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsAnonymous,
                  Status,
                  TimeZone,
                  CreateOn,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastActivityDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart, Comment )
         VALUES ( @ApplicationID,
                  @Username,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsAnonymous,
                  @Satus,
                  @TimeZone,
                  @CreateOn,
                  @CreateOn,
                  @CreateOn,
                  @CreateOn,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart, '''' )

	SET @UserID = SCOPE_IDENTITY()
	
    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    SELECT 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    SELECT @ErrorCode

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[CreateRole]    Script Date: 10/20/2011 13:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Create Role
-- =============================================
CREATE PROCEDURE [dbo].[CreateRole]
    @ApplicationName	nvarchar(100),
	@RoleName			nvarchar(256)
AS
BEGIN

    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
	DECLARE @TranStarted   bit
	SET @TranStarted = 0
	
	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END
	
	IF  EXISTS (Select * From Roles
				Where Roles.ApplicationID = @ApplicationID
				  And Roles.RoleName = @RoleName) Begin
		SELECT (1) As Result
		RETURN
	End
	
	Insert Into Roles (RoleName, ApplicationID)
		       Values (@RoleName, @ApplicationID)
		       
	
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	Select (0) As Result
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SetUserLoginDate]    Script Date: 05/09/2011 10:55:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUserLoginDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Set User Login and Activity Date
-- =============================================
CREATE PROCEDURE [dbo].[SetUserLoginDate] 
	-- Add the parameters for the stored procedure here
	@UserID int, 
	@LoginOn datetime
AS
BEGIN
	UPDATE Users SET Users.LastLoginDate = @LoginOn, Users.LastActivityDate = @LoginOn
	WHERE Users.UserID = @UserID
	
	SELECT @@ROWCOUNT
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[FindUsersByName]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindUsersByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Find the Users by Email address
-- =============================================
CREATE PROCEDURE [dbo].[FindUsersByName]
	@ApplicationName nvarchar(100), 
	@Username nvarchar(256),
	@PageIndex int,
	@PageSize int,
	@TotalRecord int OUTPUT
AS
BEGIN
	WITH SearchQuery AS(
		Select Users.*, ROW_NUMBER() OVER (ORDER BY UserID) AS RowNumber
		FROM Users Where Username LIKE @Username AND
		Users.ApplicationID = (Select Applications.ApplicationID From Applications Where Applications.Name = @ApplicationName)
	)
	
	SELECT @TotalRecord = COUNT(*) FROM SearchQuery
	
	SELECT * 
	FROM SearchQuery 
	WHERE RowNumber BETWEEN (@PageIndex * @PageSize) AND @PageSize;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[FindUsersByEmail]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindUsersByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Find the Users by Email address
-- =============================================
CREATE PROCEDURE [dbo].[FindUsersByEmail] 
	@ApplicationName nvarchar(100), 
	@Email nvarchar(256),
	@PageIndex int,
	@PageSize int,
	@TotalRecord int OUTPUT
AS
BEGIN
	WITH SearchQuery AS(
		Select Users.*, ROW_NUMBER() OVER (ORDER BY UserID) AS RowNumber
		FROM Users Where Email LIKE @Email AND
		Users.ApplicationID = (Select Applications.ApplicationID From Applications Where Applications.Name = @ApplicationName)
	)
	
	SELECT @TotalRecord = COUNT(*) FROM SearchQuery
	
	SELECT * 
	FROM SearchQuery 
	WHERE RowNumber BETWEEN (@PageIndex * @PageSize) AND @PageSize;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[IsRoleExists]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsRoleExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Check role exist in the database
-- =============================================
CREATE PROCEDURE [dbo].[IsRoleExists]
    @ApplicationName	nvarchar(100),
    @RoleName			nvarchar(256)
AS
BEGIN

	DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    SELECT DISTINCT Roles.RoleName
    FROM Roles
    WHERE Roles.ApplicationID = @ApplicationID And
		  Roles.RoleName = @RoleName
    
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsersInRole]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUsersInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	List of the users roles
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersInRole]
    @ApplicationName	nvarchar(100),
    @RoleName			nvarchar(256)
AS
BEGIN
	
    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    SELECT DISTINCT Users.Username
    FROM Roles Inner Join UsersInRoles On Roles.RoleID = UsersInRoles.Roles_RoleID Inner Join
		 Users On UsersInRoles.Users_UserID = Users.UserID    
    WHERE Roles.ApplicationID = @ApplicationID And
		  Roles.RoleName = @RoleName
    
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Updated by: http://www.codeplex.com/site/users/view/billvo
-- Create date: 02/09/2011
-- Description:	Delete the User and relation
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUser] 
	-- Add the parameters for the stored procedure here
	@ApplicationName nvarchar(100), 
	@Username nvarchar(256),
	@DeleteRelations bit
AS
BEGIN
    DECLARE @ErrorCode   int
    SET @ErrorCode = 0
    
    DECLARE @UserID int
    SELECT @UserID = Users.UserID From Users Where Username = @Username
    
	BEGIN TRAN
	If (@DeleteRelations = 1) Begin
		Delete From UsersInRoles Where UsersInRoles.Users_UserID = @UserID
		
		SET @ErrorCode = @@ERROR
		IF( @ErrorCode <> 0 )
            GOTO Cleanup
	End
	
	Delete From Users Where Users.UserID = @UserID
	
	SET @ErrorCode = @@ERROR
	IF( @ErrorCode <> 0 )
        GOTO Cleanup
        
    SELECT (0)
    COMMIT TRAN		-- Added by billvo
    GOTO ExitPoint	-- Added by billvo
    
Cleanup:

	ROLLBACK TRANSACTION
    SELECT @ErrorCode ErrCode
    
ExitPoint:			-- Added by billvo
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUsersFromRoles]    Script Date: 05/09/2011 10:55:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveUsersFromRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Remove Users from Roles
-- =============================================
Create PROCEDURE [dbo].[RemoveUsersFromRoles]
    @ApplicationName	nvarchar(100),
	@UsersForAdd		nvarchar(4000),
	@RolesForAdd		nvarchar(4000)
AS
BEGIN

    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
	DECLARE @TranStarted   bit
	SET @TranStarted = 0
	
	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END
	
	Delete From UsersInRoles
	Where
		UsersInRoles.Users_UserID In (
			Select Users.UserID From Users
			Where Users.Username In (dbo.Split(@UsersForAdd, '','')) And
				  Users.ApplicationID = @ApplicationID
		) And
		UsersInRoles.Users_UserID In (
			Select Roles.RoleID From Roles
			Where Roles.RoleName In (dbo.Split(@RolesForAdd, '','')) And
				  Roles.ApplicationID = @ApplicationID
		)
	
	
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[IsUserInRole]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsUserInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Check user exist in the role
-- =============================================
CREATE PROCEDURE [dbo].[IsUserInRole]
    @ApplicationName	nvarchar(100),
    @RoleName			nvarchar(256),
    @Username			nvarchar(256)
AS
BEGIN

	DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    SELECT DISTINCT Users.Username
    FROM Roles Inner Join UsersInRoles On Roles.RoleID = UsersInRoles.Roles_RoleID Inner Join
		 Users On UsersInRoles.Users_UserID = Users.UserID    
    WHERE Roles.ApplicationID = @ApplicationID And
		  Users.ApplicationID = @ApplicationID And
		  Roles.RoleName = @RoleName And
		  Users.Username = @Username
    
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetRolesForUser]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRolesForUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	List of the users roles
-- =============================================
CREATE PROCEDURE [dbo].[GetRolesForUser]
    @ApplicationName	nvarchar(100),
    @Username			nvarchar(256)
AS
BEGIN
	
    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    SELECT DISTINCT Roles.RoleName 
    FROM Roles Inner Join UsersInRoles On Roles.RoleID = UsersInRoles.Roles_RoleID Inner Join
		 Users On UsersInRoles.Users_UserID = Users.UserID    
    WHERE Roles.ApplicationID = @ApplicationID And
		  Users.Username = @Username
    
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[FindUsersInRole]    Script Date: 05/09/2011 10:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindUsersInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Omid Mafakher
-- Create date: 02/09/2011
-- Description:	Find user in spt role
-- =============================================
CREATE PROCEDURE [dbo].[FindUsersInRole]
	@ApplicationName nvarchar(100), 
	@UsernameToMatch nvarchar(256),
	@RoleName nvarchar(256)
AS
BEGIN
	
    DECLARE @ApplicationID int
    EXEC CreateApplication @ApplicationName, @ApplicationId OUTPUT
    
    SELECT Users.Username 
    FROM Users Join UsersInRoles On Users.UserID = UsersInRoles.Users_UserID
		 INNER Join Roles On UsersInRoles.Roles_RoleID = Roles.RoleID
    WHERE Users.ApplicationID = @ApplicationID And 
		  Roles.ApplicationID = @ApplicationID And
		  Roles.RoleName = @RoleName And
		  Users.Username Like ''%'' + @UsernameToMatch + ''%''     
    
END
' 
END
GO
