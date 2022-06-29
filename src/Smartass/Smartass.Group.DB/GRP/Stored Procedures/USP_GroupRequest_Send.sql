/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 28.06.2022.
	Purpose:	Sends a group request from user to the group
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	DECLARE @Response VARCHAR(100);

    EXEC [GRP].[USP_GroupRequest_Send] @UserIdFrom = 1,
                                       @GroupIdTo  = 3,
                                       @IsSuccessful = @Result OUTPUT,
                                       @ResponseText = @Response OUTPUT
    
    SELECT @Result, @Response;
	===================================================================================================================
	Change Log
	-------------------------------------------------------------------------------------------------------------------
	Date			Version			Developer				Change Description
	-------------------------------------------------------------------------------------------------------------------
	28.06.2022.		1.0				Stefan Ivkovic			Initial version
	===================================================================================================================
*/
CREATE PROCEDURE [GRP].[USP_GroupRequest_Send]
(
	@UserIdFrom         INT,
    @GroupIdTo          INT,
	@IsSuccessful		BIT				OUTPUT,
    @ResponseText       VARCHAR(100)    OUTPUT
)
AS
BEGIN
	BEGIN TRY
        BEGIN TRAN
			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --
            
            IF EXISTS (SELECT * FROM [GRP].[UserGroup] WHERE UserId = @UserIdFrom AND GroupId = @GroupIdTo AND IsActive = 1 AND IsDeleted = 0)
                 BEGIN
                    RAISERROR('User is already a member of the group', 18, 3)
                 END

            IF EXISTS (SELECT * FROM [GRP].[GroupRequest] WHERE UserId = @UserIdFrom AND GroupId = @GroupIdTo)
                 BEGIN
                    RAISERROR('Group request has already been sent', 18, 3)
                 END
            
            IF EXISTS (SELECT * FROM [GRP].[GroupInvite] WHERE ToUserId = @UserIdFrom AND FromGroupId = @GroupIdTo)
                 BEGIN
                    RAISERROR('Can not send a request. Group invitation already exists.', 18, 3)
                 END

            INSERT INTO [GRP].[GroupRequest]
            (
                UserId,
                GroupId,
                CreatedDateUTC
            )
            VALUES (@UserIdFrom, @GroupIdTo, GETUTCDATE());

            SET @IsSuccessful = 1
            SET @ResponseText = 'User('+CAST(@UserIdFrom AS VARCHAR)+') successfully sent a request to join the group('+CAST(@GroupIdTo AS VARCHAR)+').' 
			
            -- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --
        COMMIT
	END TRY
	BEGIN CATCH
        IF @@TRANCOUNT > 0
			BEGIN
				SET @IsSuccessful = 0;
				ROLLBACK
			END

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