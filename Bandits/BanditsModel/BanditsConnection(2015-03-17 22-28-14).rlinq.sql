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

-- Dropping index 'idx_TeamPlayer_TeamId' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_TeamPlayer_TeamId] ON [TeamPlayer]

go

-- add column for field _guardianType
ALTER TABLE [Guardian] ADD [GuardianTypeId2] int NULL

go

-- add column for field _guardianTypeId
ALTER TABLE [Guardian] ADD [GuardianTypeId] int NULL

go

-- add column for field _person
ALTER TABLE [Guardian] ADD [PersonId2] int NULL

go

-- add column for field _person
ALTER TABLE [Player] ADD [PersonId2] int NULL

go

-- add column for field _program
ALTER TABLE [Team] ADD [ProgramId2] int NULL

go

-- add column for field _player
ALTER TABLE [TeamPlayer] ADD [PlayerId2] int NULL

go

-- add column for field _team
ALTER TABLE [TeamPlayer] ADD [TeamId2] int NULL

go

ALTER TABLE [Guardian] ADD CONSTRAINT [ref_Guardian_GuardianType] FOREIGN KEY ([GuardianTypeId2]) REFERENCES [GuardianType]([GuardianTypeId])

go

ALTER TABLE [Guardian] ADD CONSTRAINT [ref_Guardian_Person] FOREIGN KEY ([PersonId2]) REFERENCES [Person]([PersonId])

go

ALTER TABLE [Player] ADD CONSTRAINT [ref_Player_Person] FOREIGN KEY ([PersonId2]) REFERENCES [Person]([PersonId])

go

ALTER TABLE [Team] ADD CONSTRAINT [ref_Team_prgram] FOREIGN KEY ([ProgramId2]) REFERENCES [prgram]([ProgramId])

go

ALTER TABLE [TeamPlayer] ADD CONSTRAINT [ref_TeamPlayer_Player] FOREIGN KEY ([PlayerId2]) REFERENCES [Player]([PlayerId])

go

ALTER TABLE [TeamPlayer] ADD CONSTRAINT [ref_TeamPlayer_Team] FOREIGN KEY ([TeamId2]) REFERENCES [Team]([TeamId])

go

-- Index 'idx_TeamPlayer_TeamId2' was not detected in the database. It will be created
CREATE INDEX [idx_TeamPlayer_TeamId2] ON [TeamPlayer]([TeamId2])

go

