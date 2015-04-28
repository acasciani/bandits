ALTER TABLE [Auth_RoleAssignment] DROP CONSTRAINT [ref_Ath_RlAssgnmnt_Wb_7425296C]

go

ALTER TABLE [Auth_ScopeAssignment] DROP CONSTRAINT [ref_Ath_ScpAssgnmnt_A_4F0D53B0]

go

ALTER TABLE [Auth_ScopeAssignment] DROP CONSTRAINT [ref_Ath_ScpAssgnmnt_W_DC75066D]

go

-- Dropping index 'idx_Ath_RlAssignment_WebUserId' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_Ath_RlAssignment_WebUserId] ON [Auth_RoleAssignment]

go

-- Dropping index 'idx_Ath_ScpAssgnment_WebUserId' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_Ath_ScpAssgnment_WebUserId] ON [Auth_ScopeAssignment]

go

-- Dropping index 'idx_Ath_ScpAssgnmnt_RlAssgnmnt' which is not configured in OpenAccess but exists on the database.
DROP INDEX [idx_Ath_ScpAssgnmnt_RlAssgnmnt] ON [Auth_ScopeAssignment]

go

-- System.Collections.Generic.IList`1 BanditsModel.Auth_RoleAssignment._auth_ScopeAssignments
CREATE TABLE [Ath_RlAssgnmnt_Ath_ScpAssgnmnt] (
    [RoleAssignmentId] int NOT NULL,
    [ScopeAssignmentId] int NOT NULL,
    CONSTRAINT [pk_Ath_RlAssgnmnt_Ath_9AA9C166] PRIMARY KEY ([RoleAssignmentId], [ScopeAssignmentId])
)

go

-- System.Collections.Generic.IList`1 BanditsModel.Auth_RoleAssignment._auth_Permissions
CREATE TABLE [Ath_RlAssgnmnt_Auth_Permission] (
    [RoleAssignmentId] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [pk_Ath_RlAssgnmnt_Ath_6CAE006F] PRIMARY KEY ([RoleAssignmentId], [PermissionId])
)

go

-- add column for field _comments
ALTER TABLE [Auth_Permission] ADD [Comments] varchar(255) NULL

go

-- add column for field _deny
ALTER TABLE [Auth_Permission] ADD [Deny] tinyint NULL

go

UPDATE [Auth_Permission] SET [Deny] = 0

go

ALTER TABLE [Auth_Permission] ALTER COLUMN [Deny] tinyint NOT NULL

go

-- add column for field _permissionName
ALTER TABLE [Auth_Permission] ADD [PermissionName] varchar(255) NULL

go

-- dropping unknown column [nme]
ALTER TABLE [Auth_Permission] DROP COLUMN [nme]

go

-- add column for field _comments
ALTER TABLE [Auth_Role] ADD [Comments] varchar(255) NULL

go

-- add column for field _roleName
ALTER TABLE [Auth_Role] ADD [RoleName] varchar(255) NULL

go

-- dropping unknown column [nme]
ALTER TABLE [Auth_Role] DROP COLUMN [nme]

go

-- dropping unknown column [WebUserId]
ALTER TABLE [Auth_RoleAssignment] DROP COLUMN [WebUserId]

go

-- add column for field _scope
ALTER TABLE [Auth_Scope] ADD [Scope] int NULL

go

UPDATE [Auth_Scope] SET [Scope] = 0

go

ALTER TABLE [Auth_Scope] ALTER COLUMN [Scope] int NOT NULL

go

-- dropping unknown column [scpe]
ALTER TABLE [Auth_Scope] DROP COLUMN [scpe]

go

-- add column for field _deny
ALTER TABLE [Auth_ScopeAssignment] ADD [Deny] tinyint NULL

go

UPDATE [Auth_ScopeAssignment] SET [Deny] = 0

go

ALTER TABLE [Auth_ScopeAssignment] ALTER COLUMN [Deny] tinyint NOT NULL

go

-- add column for field _resourceId
ALTER TABLE [Auth_ScopeAssignment] ADD [ResourceId] bigint NULL

go

UPDATE [Auth_ScopeAssignment] SET [ResourceId] = 0

go

ALTER TABLE [Auth_ScopeAssignment] ALTER COLUMN [ResourceId] bigint NOT NULL

go

-- dropping unknown column [WebUserId]
ALTER TABLE [Auth_ScopeAssignment] DROP COLUMN [WebUserId]

go

-- dropping unknown column [RoleAssignmentId]
ALTER TABLE [Auth_ScopeAssignment] DROP COLUMN [RoleAssignmentId]

go

-- add column for field _guardianName
ALTER TABLE [GuardianType] ADD [GuardianName] varchar(255) NULL

go

-- dropping unknown column [nme]
ALTER TABLE [GuardianType] DROP COLUMN [nme]

go

-- add column for field _teamName
ALTER TABLE [Team] ADD [TeamName] varchar(255) NULL

go

-- dropping unknown column [nme]
ALTER TABLE [Team] DROP COLUMN [nme]

go

-- add column for field _password
ALTER TABLE [WebUser] ADD [Password] varchar(255) NULL

go

-- dropping unknown column [passwd]
ALTER TABLE [WebUser] DROP COLUMN [passwd]

go

-- System.Collections.Generic.IList`1 BanditsModel.WebUser._auth_Permissions
CREATE TABLE [WebUser_Auth_Permission] (
    [WebUserId] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [pk_WebUser_Auth_Permission] PRIMARY KEY ([WebUserId], [PermissionId])
)

go

-- System.Collections.Generic.IList`1 BanditsModel.WebUser._auth_RoleAssignments
CREATE TABLE [WebUser_Auth_RoleAssignment] (
    [WebUserId] int NOT NULL,
    [RoleAssignmentId] int NOT NULL,
    CONSTRAINT [pk_WebUser_Auth_RoleAssignment] PRIMARY KEY ([WebUserId], [RoleAssignmentId])
)

go

-- System.Collections.Generic.IList`1 BanditsModel.WebUser._auth_ScopeAssignments
CREATE TABLE [WebUser_Auth_ScopeAssignment] (
    [WebUserId] int NOT NULL,
    [ScopeAssignmentId] int NOT NULL,
    CONSTRAINT [pk_WbUsr_Ath_ScpAssgn_8DD3F7D1] PRIMARY KEY ([WebUserId], [ScopeAssignmentId])
)

go

-- add column for field _programName
ALTER TABLE [prgram] ADD [ProgramName] varchar(255) NULL

go

-- dropping unknown column [nme]
ALTER TABLE [prgram] DROP COLUMN [nme]

go

ALTER TABLE [Ath_RlAssgnmnt_Ath_ScpAssgnmnt] ADD CONSTRAINT [ref_Ath_RlAssgnmnt_At_D6DF8357] FOREIGN KEY ([RoleAssignmentId]) REFERENCES [Auth_RoleAssignment]([RoleAssignmentId])

go

ALTER TABLE [Ath_RlAssgnmnt_Ath_ScpAssgnmnt] ADD CONSTRAINT [ref_Ath_RlAssgnmnt_At_34A813B8] FOREIGN KEY ([ScopeAssignmentId]) REFERENCES [Auth_ScopeAssignment]([ScopeAssignmentId])

go

ALTER TABLE [Ath_RlAssgnmnt_Auth_Permission] ADD CONSTRAINT [ref_Ath_RlAssgnmnt_At_B6BA6274] FOREIGN KEY ([RoleAssignmentId]) REFERENCES [Auth_RoleAssignment]([RoleAssignmentId])

go

ALTER TABLE [Ath_RlAssgnmnt_Auth_Permission] ADD CONSTRAINT [ref_Ath_RlAssgnmnt_At_E2610E1E] FOREIGN KEY ([PermissionId]) REFERENCES [Auth_Permission]([PermissionId])

go

ALTER TABLE [WebUser_Auth_Permission] ADD CONSTRAINT [ref_WbUsr_Ath_Prmssn__E596CF26] FOREIGN KEY ([WebUserId]) REFERENCES [WebUser]([WebUserId])

go

ALTER TABLE [WebUser_Auth_Permission] ADD CONSTRAINT [ref_WbUsr_Ath_Prmssn__FCA81F6C] FOREIGN KEY ([PermissionId]) REFERENCES [Auth_Permission]([PermissionId])

go

ALTER TABLE [WebUser_Auth_RoleAssignment] ADD CONSTRAINT [ref_WbUsr_Ath_RlAssgn_C63B5B4A] FOREIGN KEY ([WebUserId]) REFERENCES [WebUser]([WebUserId])

go

ALTER TABLE [WebUser_Auth_RoleAssignment] ADD CONSTRAINT [ref_WbUsr_Ath_RlAssgn_29130E18] FOREIGN KEY ([RoleAssignmentId]) REFERENCES [Auth_RoleAssignment]([RoleAssignmentId])

go

ALTER TABLE [WebUser_Auth_ScopeAssignment] ADD CONSTRAINT [ref_WbUsr_Ath_ScpAssg_0ECA9DE2] FOREIGN KEY ([WebUserId]) REFERENCES [WebUser]([WebUserId])

go

ALTER TABLE [WebUser_Auth_ScopeAssignment] ADD CONSTRAINT [ref_WbUsr_Ath_ScpAssg_079FB5B2] FOREIGN KEY ([ScopeAssignmentId]) REFERENCES [Auth_ScopeAssignment]([ScopeAssignmentId])

go

