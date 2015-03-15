ALTER TABLE [Team] DROP CONSTRAINT [ref_Team_Program]

go

-- dropping table [Program]
DROP TABLE [Program]

go

-- BanditsModel.Program
CREATE TABLE [prgram] (
    [ProgramId] int IDENTITY NOT NULL,      -- _programId
    [nme] varchar(255) NULL,                -- _name
    CONSTRAINT [pk_prgram] PRIMARY KEY ([ProgramId])
)

go

