-- BanditsModel.Auth_Permission
CREATE TABLE [Auth_Permission] (
    [nme] varchar(255) NULL,                -- _name
    [PermissionId] int NOT NULL,            -- _permissionId
    CONSTRAINT [pk_Auth_Permission] PRIMARY KEY ([PermissionId])
)

go

-- BanditsModel.Auth_PermissionAssignment
CREATE TABLE [Auth_PermissionAssignment] (
    [PermissionId] int NULL,                -- _auth_Permission
    [RoleAssignmentId] int NULL,            -- _auth_RoleAssignment
    [PermissionAssignmentId] int NOT NULL,  -- _permissionAssignmentId
    [WebUserId] int NULL,                   -- _webUser
    CONSTRAINT [pk_Auth_PermissionAssignment] PRIMARY KEY ([PermissionAssignmentId])
)

go

-- BanditsModel.Auth_Role
CREATE TABLE [Auth_Role] (
    [nme] varchar(255) NULL,                -- _name
    [RoleId] int NOT NULL,                  -- _roleId
    CONSTRAINT [pk_Auth_Role] PRIMARY KEY ([RoleId])
)

go

-- BanditsModel.Auth_RoleAssignment
CREATE TABLE [Auth_RoleAssignment] (
    [RoleId] int NULL,                      -- _auth_Role
    [RoleAssignmentId] int NOT NULL,        -- _roleAssignmentId
    [WebUserId] int NULL,                   -- _webUser
    CONSTRAINT [pk_Auth_RoleAssignment] PRIMARY KEY ([RoleAssignmentId])
)

go

-- BanditsModel.Auth_Scope
CREATE TABLE [Auth_Scope] (
    [scpe] int NOT NULL,                    -- _scope
    [ScopeId] int NOT NULL,                 -- _scopeId
    CONSTRAINT [pk_Auth_Scope] PRIMARY KEY ([ScopeId])
)

go

-- BanditsModel.Auth_ScopeAssignment
CREATE TABLE [Auth_ScopeAssignment] (
    [ScopeId] int NULL,                     -- _auth_Scope
    [ScopeAssignmentId] int NOT NULL,       -- _scopeAssignmentId
    [WebUserId] int NULL,                   -- _webUser
    [RoleAssignmentId] int NULL,            -- Auth_RoleAssignment__auth_ScopeAssignments
    CONSTRAINT [pk_Auth_ScopeAssignment] PRIMARY KEY ([ScopeAssignmentId])
)

go

ALTER TABLE [Auth_PermissionAssignment] ADD CONSTRAINT [ref_Ath_PrmssnAssgnmn_95A4CA8B] FOREIGN KEY ([PermissionId]) REFERENCES [Auth_Permission]([PermissionId])

go

ALTER TABLE [Auth_PermissionAssignment] ADD CONSTRAINT [ref_Ath_PrmssnAssgnmn_D26E8AEA] FOREIGN KEY ([RoleAssignmentId]) REFERENCES [Auth_RoleAssignment]([RoleAssignmentId])

go

ALTER TABLE [Auth_PermissionAssignment] ADD CONSTRAINT [ref_Ath_PrmssnAssgnmn_DC357B25] FOREIGN KEY ([WebUserId]) REFERENCES [WebUser]([WebUserId])

go

ALTER TABLE [Auth_RoleAssignment] ADD CONSTRAINT [ref_Ath_RlAssgnmnt_At_5C0BEE08] FOREIGN KEY ([RoleId]) REFERENCES [Auth_Role]([RoleId])

go

ALTER TABLE [Auth_RoleAssignment] ADD CONSTRAINT [ref_Ath_RlAssgnmnt_Wb_7425296C] FOREIGN KEY ([WebUserId]) REFERENCES [WebUser]([WebUserId])

go

ALTER TABLE [Auth_ScopeAssignment] ADD CONSTRAINT [ref_Ath_ScpAssgnmnt_A_DBAD34C5] FOREIGN KEY ([ScopeId]) REFERENCES [Auth_Scope]([ScopeId])

go

ALTER TABLE [Auth_ScopeAssignment] ADD CONSTRAINT [ref_Ath_ScpAssgnmnt_W_DC75066D] FOREIGN KEY ([WebUserId]) REFERENCES [WebUser]([WebUserId])

go

ALTER TABLE [Auth_ScopeAssignment] ADD CONSTRAINT [ref_Ath_ScpAssgnmnt_A_4F0D53B0] FOREIGN KEY ([RoleAssignmentId]) REFERENCES [Auth_RoleAssignment]([RoleAssignmentId])

go

-- Index 'idx_Ath_PrmssnAssgnmnt_RlAssgn' was not detected in the database. It will be created
CREATE INDEX [idx_Ath_PrmssnAssgnmnt_RlAssgn] ON [Auth_PermissionAssignment]([RoleAssignmentId])

go

-- Index 'idx_Ath_PrmssnAssgnmnt_WbUsrId' was not detected in the database. It will be created
CREATE INDEX [idx_Ath_PrmssnAssgnmnt_WbUsrId] ON [Auth_PermissionAssignment]([WebUserId])

go

-- Index 'idx_Ath_RlAssignment_WebUserId' was not detected in the database. It will be created
CREATE INDEX [idx_Ath_RlAssignment_WebUserId] ON [Auth_RoleAssignment]([WebUserId])

go

-- Index 'idx_Ath_ScpAssgnmnt_RlAssgnmnt' was not detected in the database. It will be created
CREATE INDEX [idx_Ath_ScpAssgnmnt_RlAssgnmnt] ON [Auth_ScopeAssignment]([RoleAssignmentId])

go

-- Index 'idx_Ath_ScpAssgnment_WebUserId' was not detected in the database. It will be created
CREATE INDEX [idx_Ath_ScpAssgnment_WebUserId] ON [Auth_ScopeAssignment]([WebUserId])

go

