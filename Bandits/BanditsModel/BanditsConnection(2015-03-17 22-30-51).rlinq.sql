-- add column for field _createUser
ALTER TABLE [Person] ADD [CreateUser_WebUserId2] int NULL

go

-- add column for field _modifyUser
ALTER TABLE [Person] ADD [ModifyUser_WebUserId2] int NULL

go

-- add column for field _person
ALTER TABLE [WebUser] ADD [PersonId2] int NULL

go

