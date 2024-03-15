CREATE TABLE [dbo].[my_books] (
    [id]      INT            IDENTITY(1, 1),
    [user_id] NVARCHAR (255) NOT NULL,
    [book_id] INT            NOT NULL,
    CONSTRAINT [PK_my_books] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_my_books_books] FOREIGN KEY ([book_id]) REFERENCES [dbo].[books] ([id])
);

