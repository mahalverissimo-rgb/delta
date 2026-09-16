declare commands cursor for
SELECT
	'ALTER TABLE [' + T.Name + '] ALTER COLUMN [' + C.Name + '] INT NULL'
FROM 
	sysobjects T
	INNER JOIN Syscolumns C ON T.id = C.id
WHERE 
	C.Name = 'ContratoAditivoId'
	AND T.Name LIKE '%Selecionado'

declare @cmd varchar(max)

open commands
fetch next from commands into @cmd
while @@FETCH_STATUS=0
begin
  exec(@cmd)
  fetch next from commands into @cmd
end

close commands
deallocate commands



