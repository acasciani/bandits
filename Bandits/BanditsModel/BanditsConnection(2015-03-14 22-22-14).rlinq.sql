-- Dropping index 'idx_Team_ProgramId' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_Team_ProgramId] ON [Team]

go

-- Column was read from database as: [FName] varchar(75) null
-- modify column for field _fName
ALTER TABLE [Person] ALTER COLUMN [FName] varchar(255) NULL

go

-- Column was read from database as: [LName] varchar(75) null
-- modify column for field _lName
ALTER TABLE [Person] ALTER COLUMN [LName] varchar(255) NULL

go

ALTER TABLE [Team] ADD CONSTRAINT [ref_Team_prgram] FOREIGN KEY ([ProgramId]) REFERENCES [prgram]([ProgramId])

go

