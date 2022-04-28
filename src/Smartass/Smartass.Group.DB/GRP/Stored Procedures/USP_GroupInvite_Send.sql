/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 28.04.2022.
	Purpose:	Sends a group invitaion to the user
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	DECLARE @Response VARCHAR(100);

    EXEC [GRP].[USP_GroupInvite_Send] @GroupIdFrom = 1,
                                      @UserIdFrom  =3,
                                      @UserIdTo = 10,
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
CREATE PROCEDURE [GRP].[USP_GroupInvite_Send]
(
    @GroupIdFrom        INT,
	@UserIdFrom         INT,
    @UserIdTo           INT,
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
            
			-- Begin Adding Invitation --
            INSERT INTO [GRP].[GroupInvite]
            (
                FromUserId,
                ToUserId,
                FromGroupId,
                CreatedDateUTC
            )
            VALUES (@UserIdFrom, @UserIdTo, @GroupIdFrom, GETUTCDATE());
			-- End Adding --

			-- Begin Response handling --
            SET @IsSuccessful = 1
            SET @ResponseText = 'User('+CAST(@UserIdFrom AS VARCHAR)+') successfully sent a group invitation to user('+CAST(@UserIdTo AS VARCHAR)+') to join group('+CAST(@GroupIdFrom AS VARCHAR)+').'
			-- End Response handling --

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

		SELECT @ErrorMessage = ERROR_MESSAGE() + 'Line: ' + CAST(ERROR_LINE() AS NVARCHAR(5)),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();

		-- Start Error logging --
		-- TO-DO: Implement Error logging
		-- End Error logging --

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH
END