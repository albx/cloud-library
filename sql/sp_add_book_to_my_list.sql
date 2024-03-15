SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_add_book_to_my_list]
	@userId nvarchar(255),
	@bookId int
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @existAlready int;
	SET @existAlready = (SELECT COUNT(book_id) FROM [dbo].[my_books] WHERE [user_id]=@userId AND [book_id]=@bookId);

	IF @existAlready = 0
	BEGIN
		INSERT INTO [dbo].[my_books]([user_id], [book_id]) VALUES (@userId, @bookId)
	END
END
GO