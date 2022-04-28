/*
	===================================================================================================================
	Stored procedure info
	-------------------------------------------------------------------------------------------------------------------
	Created On: 28.04.2022.
	Purpose:	Gets list of all groups where the user is active member and where group name contains search text.
	Module:		Group/Group
	===================================================================================================================
	Test SQL
	-------------------------------------------------------------------------------------------------------------------
	DECLARE @Result BIT;
	
    EXEC [GRP].[USP_GroupListByUser_Search] @UserId=3,
                                            @SearchText='VISER',
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
CREATE PROCEDURE [GRP].[USP_GroupListByUser_Search]
(
	@UserId             INT,
    @SearchText         NVARCHAR(40),
	@IsSuccessful		BIT				OUTPUT
)
AS
BEGIN
	BEGIN TRY
			-- Begin Execution Logging --
			-- TO-DO: Implement Execution Logging
			-- End Logging --
            SELECT
                g.GroupId,
                g.GroupName,
                g.ProfileImage
            FROM
                [GRP].[Group] g
                JOIN [GRP].[UserGroup] ug ON g.GroupId = ug.GroupId
            WHERE
                ug.UserId = @UserId
                AND g.GroupName LIKE '%'+@SearchText+'%'
                AND ug.IsActive = 1
                AND ug.IsDeleted = 0
                AND g.IsActive = 1
                AND g.IsDeleted = 0

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