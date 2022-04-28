/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 28.04.2022.
	Purpose:	Gets list of all group invites.
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	
    EXEC [GRP].[USP_GroupInviteListByUser_Get] @UserId=5,
                                               @IsSuccessful=@Result OUTPUT
    
    SELECT @Result;
	===================================================================================================================
	Change Log
	-------------------------------------------------------------------------------------------------------------------
	Date			Version			Developer				Change Description
	-------------------------------------------------------------------------------------------------------------------
	28.04.2022.		1.0				Stefan Ivkovic			Initial version
	===================================================================================================================
*/
CREATE PROCEDURE [GRP].[USP_GroupInviteListByUser_Get]
(
	@UserId             INT,
	@IsSuccessful		BIT				OUTPUT
)
AS
BEGIN
	BEGIN TRY
			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --

            -- Begin Main Query --
            SELECT
                g.GroupId,
                g.GroupName,
                g.ProfileImage AS 'GroupImage',
                u.UserId,
                u.FirstName,
                u.LastName,
                u.ProfileImage,
                gi.CreatedDateUTC
            FROM
                [GRP].[Group] g
                JOIN [GRP].[GroupInvite] gi ON g.GroupId = gi.FromGroupId
                JOIN [USR].[User] u ON gi.FromUserId = u.UserId
            WHERE
                gi.ToUserId = @UserId
                AND g.IsActive = 1
                AND g.IsDeleted = 0
            -- End Main Query --

            SET @IsSuccessful = 1

			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --
	END TRY
	BEGIN CATCH
        SET @IsSuccessful = 0

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = ERROR_MESSAGE() + 'Line: ' + CAST(ERROR_LINE() AS NVARCHAR(5)),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();

		-- Start Error logging --
		-- TO-DO: Implement Error logging
		-- End Error logging --

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH
END