Write-Output "Aggiungo la tabella my_books alle entities"
dab add MyBooks --source dbo.my_books --permissions "authenticated:read" --config .\swa-db-connections\staticwebapp.database.config.json
dab update MyBooks --relationship "books" --cardinality "one" --target.entity "Book" --config .\swa-db-connections\staticwebapp.database.config.json

Write-Output "Aggiungo la stored procedure che inserisce il libro nella mia lista"
dab add AddBookToMyList --source dbo.sp_add_book_to_my_list --source.type "stored-procedure" --source.params "userId:userId,bookId:bookId" --permissions "authenticated:execute" --rest.methods "post" --graphql false --config .\swa-db-connections\staticwebapp.database.config.json