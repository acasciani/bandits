ALTER TABLE [Player] DROP CONSTRAINT [ref_Player_Person]

go

-- Column was read from database as: [PersonId] int null
-- modify column for field _person
UPDATE [Player]
   SET [PersonId] = 0 -- Add your own default value here, for when [PersonId] is null.
 WHERE [PersonId] IS NULL

go

ALTER TABLE [Player] ALTER COLUMN [PersonId] int NOT NULL

go

ALTER TABLE [Player] ADD CONSTRAINT [ref_Player_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId])

go

