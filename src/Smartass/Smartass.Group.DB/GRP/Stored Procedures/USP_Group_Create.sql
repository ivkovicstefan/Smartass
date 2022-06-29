/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 24.04.2022.
	Purpose:	Creates a new group and creates membership for the owner
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	DECLARE @Response NVARCHAR(100);
	DECLARE @ReferenceId INT;

	EXEC GRP.USP_Group_Create @GroupName='FON Inzenjeri', 
							  @GroupDescription='VISER', 
							  @CreatedByUserId = 10,
							  @IsSuccessful = @Result OUTPUT,
							  @ResponseText = @Response OUTPUT,
							  @GroupId = @ReferenceId OUTPUT

	SELECT @Result AS 'Is Successful',
		   @Response AS 'Response Text', 
		   @ReferenceId AS 'Group Id'
	===================================================================================================================
	Change Log
	-------------------------------------------------------------------------------------------------------------------
	Date			Version			Developer				Change Description
	-------------------------------------------------------------------------------------------------------------------
	24.02.2022.		1.0				Stefan Ivkovic			Initial version
	===================================================================================================================
*/
CREATE PROCEDURE [GRP].[USP_Group_Create]
(
	@GroupName			NVARCHAR(40),
	@GroupDescription	NVARCHAR(400) = NULL,
	@CreatedByUserId	INT,
	@IsSuccessful		BIT				OUTPUT,
	@ResponseText		VARCHAR(100)	OUTPUT,
	@GroupId			INT				OUTPUT
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --

			-- Begin Verification of input strings --
			-- TO-DO: Implement Input String Verification
			-- End Verification --

			-- Begin Creating new group --
			INSERT INTO [GRP].[Group]
			(
				GroupName,
				GroupDescription,
				IsActive,
				IsDeleted,
				CreatedByUserId,
				CreatedDateUTC
			)
			VALUES (@GroupName, @GroupDescription, 1, 0, @CreatedByUserId, GETUTCDATE());

			SET @GroupId = SCOPE_IDENTITY();
			-- End Creating --

			-- Begin Adding membership --
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
			VALUES (@CreatedByUserId, @GroupId, 1, 1, 0, 1, GETUTCDATE())
			-- End Adding --

			-- Begin Response handling --
			SET @IsSuccessful = 1;
			SET @ResponseText = 'Group('+ CAST(@GroupId AS NVARCHAR(30)) +') has been successfully created by user('+ CAST(@CreatedByUserId AS NVARCHAR(30)) +').'
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

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();

		-- Start Error logging --
		-- TO-DO: Implement Error logging
		-- End Error logging --

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH
END