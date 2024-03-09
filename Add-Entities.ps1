Write-Output "Aggiungo l'entità Book"
dab add Book --source dbo.books --permissions "anonymous:read" --config .\swa-db-connections\staticwebapp.database.config.json

Write-Output "Aggiungo l'entità Author"
dab add Author --source dbo.authors --permissions "anonymous:read" --config .\swa-db-connections\staticwebapp.database.config.json

Write-Output "Aggiungo la relazione tra le entità"
dab update Book --relationship "authors" --cardinality "many" --target.entity "Author" --linking.object "dbo.books_authors" --config .\swa-db-connections\staticwebapp.database.config.json
dab update Author --relationship "books" --cardinality "many" --target.entity "Book" --linking.object "dbo.books_authors" --config .\swa-db-connections\staticwebapp.database.config.json
