SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[vw_my_books_lookup]
as
with aggregated_authors as 
(
    select 
        ba.book_id,
        string_agg(concat(a.first_name, ' ', (a.middle_name + ' '), a.last_name), ', ') as authors
    from
        dbo.books_authors ba 
    inner join 
        dbo.authors a on ba.author_id = a.id
    group by
        ba.book_id    
)
select
    mb.id as id,
    b.id as book_id,
    b.title as title,
    b.pages as pages,
    b.[year] as [year],
    aa.authors as authors,
    mb.user_id as user_id
from
    dbo.books b
inner join
    aggregated_authors aa on b.id = aa.book_id
inner join
    dbo.my_books mb on b.id = mb.book_id
GO