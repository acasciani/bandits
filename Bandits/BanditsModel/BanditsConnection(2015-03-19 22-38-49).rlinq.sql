ALTER TABLE [Player] DROP CONSTRAINT [ref_Player_Person]

go

-- Column was read from database as: [PersonId] int not null
-- modify column for field _person
ALTER TABLE [Player] ALTER COLUMN [PersonId] int NULL

go

ALTER TABLE [Player] ADD CONSTRAINT [ref_Player_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId])

go

