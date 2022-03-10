-- Author:		Steph
-- Create date: 05/03/2022
-- Description:	Stored Procedure responsible of getting All User Informations
-- =============================================
CREATE PROCEDURE GetApplicationUserInfos
	-- Add the parameters for the stored procedure here
	@ApplicationUserID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
			E.ApplicationUserID,
			E.EmployeeID,
			E.FirstName,
			E.LastName,
			E.DoB
	FROM Employees E
	WHERE E.ApplicationUserID = @ApplicationUserID
END