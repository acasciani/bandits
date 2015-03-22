ALTER TABLE [Guardian] DROP CONSTRAINT [ref_Guardian_GuardianType]

go

ALTER TABLE [Guardian] DROP CONSTRAINT [ref_Guardian_Person]

go

ALTER TABLE [Player] DROP CONSTRAINT [ref_Player_Person]

go

ALTER TABLE [Team] DROP CONSTRAINT [ref_Team_prgram]

go

ALTER TABLE [TeamPlayer] DROP CONSTRAINT [ref_TeamPlayer_Player]

go

ALTER TABLE [TeamPlayer] DROP CONSTRAINT [ref_TeamPlayer_Team]

go

-- Dropping index 'idx_TeamPlayer_TeamId2' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_TeamPlayer_TeamId2] ON [TeamPlayer]

go

-- dropping unknown column [GuardianTypeId2]
ALTER TABLE [Guardian] DROP COLUMN [GuardianTypeId2]

go

-- dropping unknown column [PersonId2]
ALTER TABLE [Guardian] DROP COLUMN [PersonId2]

go

-- dropping unknown column [CreateUser_WebUserId2]
ALTER TABLE [Person] DROP COLUMN [CreateUser_WebUserId2]

go

-- dropping unknown column [ModifyUser_WebUserId2]
ALTER TABLE [Person] DROP COLUMN [ModifyUser_WebUserId2]

go

-- dropping unknown column [PersonId2]
ALTER TABLE [Player] DROP COLUMN [PersonId2]

go

-- dropping unknown column [ProgramId2]
ALTER TABLE [Team] DROP COLUMN [ProgramId2]

go

-- dropping unknown column [PlayerId2]
ALTER TABLE [TeamPlayer] DROP COLUMN [PlayerId2]

go

-- dropping unknown column [TeamId2]
ALTER TABLE [TeamPlayer] DROP COLUMN [TeamId2]

go

-- dropping unknown column [PersonId2]
ALTER TABLE [WebUser] DROP COLUMN [PersonId2]

go

ALTER TABLE [Guardian] ADD CONSTRAINT [ref_Guardian_GuardianType] FOREIGN KEY ([GuardianTypeId]) REFERENCES [GuardianType]([GuardianTypeId])

go

ALTER TABLE [Guardian] ADD CONSTRAINT [ref_Guardian_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId])

go

ALTER TABLE [Player] ADD CONSTRAINT [ref_Player_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId])

go

ALTER TABLE [Team] ADD CONSTRAINT [ref_Team_prgram] FOREIGN KEY ([ProgramId]) REFERENCES [prgram]([ProgramId])

go

ALTER TABLE [TeamPlayer] ADD CONSTRAINT [ref_TeamPlayer_Player] FOREIGN KEY ([PlayerId]) REFERENCES [Player]([PlayerId])

go

ALTER TABLE [TeamPlayer] ADD CONSTRAINT [ref_TeamPlayer_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team]([TeamId])

go

-- Index 'idx_TeamPlayer_TeamId' was not detected in the database. It will be created
CREATE INDEX [idx_TeamPlayer_TeamId] ON [TeamPlayer]([TeamId])

go

