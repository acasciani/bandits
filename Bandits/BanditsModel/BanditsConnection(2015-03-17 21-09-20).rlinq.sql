ALTER TABLE [UsersOpenAuthAccounts] DROP CONSTRAINT [OpenAuthUserData_Accounts]

go

-- dropping table [UsersOpenAuthAccounts]
DROP TABLE [UsersOpenAuthAccounts]

go

-- dropping table [UsersOpenAuthData]
DROP TABLE [UsersOpenAuthData]

go

-- BanditsModel.GuardianType
CREATE TABLE [GuardianType] (
    [nme] varchar(255) NULL,                -- _name
    [GuardianTypeId] int NOT NULL,          -- _guardianTypeId
    CONSTRAINT [pk_GuardianType] PRIMARY KEY ([GuardianTypeId])
)

go

