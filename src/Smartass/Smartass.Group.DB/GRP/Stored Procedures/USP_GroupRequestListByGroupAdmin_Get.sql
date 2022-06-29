/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 29.06.2022.
	Purpose:	Gets list of all group requests for the user.
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	
    EXEC [GRP].[USP_GroupRequestListByGroupAdmin_Get] @UserId=5,
                                                      @GroupId=3,
                                                      @IsSuccessful=@Result OUTPUT
    
    SELECT @Result;
	===================================================================================================================
	Change Log
	-------------------------------------------------------------------------------------------------------------------
	Date			Version			Developer				Change Description
	-------------------------------------------------------------------------------------------------------------------
	29.06.2022.		1.0				Stefan Ivkovic			Initial version
	===================================================================================================================
*/
CREATE PROCEDURE [GRP].[USP_GroupRequestListByGroupAdmin_Get]
(
	@UserId             INT,
    @GroupId            INT,
	@IsSuccessful		BIT				OUTPUT
)
AS
BEGIN
	BEGIN TRY
			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --

            IF (SELECT IsAdmin FROM [GRP].[UserGroup] WHERE UserId = @UserId) = 0
                BEGIN
                    RAISERROR('Not Authorized', 18, 3)
                END

            -- Begin Main Query --
            SELECT
                gr.GroupRequestId,
                u.UserId,
                u.FirstName,
                u.LastName,
                u.ProfileImage,
                gr.CreatedDateUTC
            FROM
                [GRP].[GroupRequest] gr
                JOIN [USR].[User] u ON gr.UserId = u.UserId
            WHERE
                gr.GroupId = @GroupId
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

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();

		-- Start Error logging --
		-- TO-DO: Implement Error logging
		-- End Error logging --

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH
END