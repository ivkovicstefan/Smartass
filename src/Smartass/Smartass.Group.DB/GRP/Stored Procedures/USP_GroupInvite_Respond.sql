/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 28.04.2022.
	Purpose:	Accepts or declines an invitation from the group
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	DECLARE @Response VARCHAR(100);

    EXEC [GRP].[USP_GroupInvite_Respond] @GroupInviteId = 1,
                                         @IsInvitationAccepted = 1,
                                         @IsSuccessful = @Result OUTPUT,
                                         @ResponseText = @Response OUTPUT
    
    SELECT @Result, @Response;
	===================================================================================================================
	Change Log
	-------------------------------------------------------------------------------------------------------------------
	Date			Version			Developer				Change Description
	-------------------------------------------------------------------------------------------------------------------
	28.04.2022.		1.0				Stefan Ivkovic			Initial version
	===================================================================================================================
*/
CREATE PROCEDURE [GRP].[USP_GroupInvite_Respond]
(
    @GroupInviteId              INT,
    @IsInvitationAccepted       INT,
	@IsSuccessful               BIT             OUTPUT,
    @ResponseText               VARCHAR(100)    OUTPUT
)
AS
BEGIN
	BEGIN TRY
        BEGIN TRAN
			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --
            
            DECLARE  @ToUserId INT 
                = (SELECT ToUserId FROM [GRP].[GroupInvite] WHERE GroupInviteId = @GroupInviteId)

            DECLARE  @FromUserId INT 
                = (SELECT FromUserId FROM [GRP].[GroupInvite] WHERE GroupInviteId = @GroupInviteId)

            DECLARE  @FromGroupId INT 
                = (SELECT FromGroupId FROM [GRP].[GroupInvite] WHERE GroupInviteId = @GroupInviteId)

            IF (@IsInvitationAccepted = 1)
                BEGIN
                    INSERT INTO [GRP].[UserGroup]
                    (
                        UserId,
                        GroupId,
                        IsAdmin,
                        IsActive,
                        IsDeleted,
                        CreatedByUserId,
                        CreatedDateUTC
                    )
                    VALUES (@ToUserId, @FromGroupId, 0, 1, 0, @FromUserId, GETUTCDATE());
                END

            DELETE 
            FROM 
                [GRP].[GroupInvite]
            WHERE
                GroupInviteId = @GroupInviteId

            SET @IsSuccessful = 1
            SET @ResponseText = 'User('+CAST(@FromUserId AS VARCHAR)+') responded to the group invitation.'

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