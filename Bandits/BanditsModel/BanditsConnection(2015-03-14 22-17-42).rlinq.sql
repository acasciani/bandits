ALTER TABLE [Guardian] DROP CONSTRAINT [ref_Guardian_Player]

go

ALTER TABLE [Team] DROP CONSTRAINT [ref_Team_prgram]

go

-- Dropping index 'idx_Guardian_PlayerId' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_Guardian_PlayerId] ON [Guardian]

go

-- dropping table [prgram]
DROP TABLE [prgram]

go

-- dropping unknown column [PlayerId]
ALTER TABLE [Guardian] DROP COLUMN [PlayerId]

go

-- System.Collections.Generic.IList`1 BanditsModel.Player._guardians
CREATE TABLE [Player_Guardian] (
    [PlayerId] int NOT NULL,
    [GuardianId] int NOT NULL,
    CONSTRAINT [pk_Player_Guardian] PRIMARY KEY ([PlayerId], [GuardianId])
)

go

-- BanditsModel.Program
CREATE TABLE [Program] (
    [ProgramId] int IDENTITY NOT NULL,      -- _programId
    [nme] varchar(255) NULL,                -- _name
    CONSTRAINT [pk_prgram_5D504E18] PRIMARY KEY ([ProgramId])
)

go

ALTER TABLE [Player_Guardian] ADD CONSTRAINT [ref_Player_Guardian_Player] FOREIGN KEY ([PlayerId]) REFERENCES [Player]([PlayerId])

go

ALTER TABLE [Player_Guardian] ADD CONSTRAINT [ref_Player_Guardian_Guardian] FOREIGN KEY ([GuardianId]) REFERENCES [Guardian]([GuardianId])

go

ALTER TABLE [Team] ADD CONSTRAINT [ref_Team_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([ProgramId])

go

-- Index 'idx_Player_Guardian_GuardianId' was not detected in the database. It will be created
CREATE INDEX [idx_Player_Guardian_GuardianId] ON [Player_Guardian]([GuardianId])

go

