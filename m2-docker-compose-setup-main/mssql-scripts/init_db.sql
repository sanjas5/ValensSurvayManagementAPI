 CREATE TABLE [TEST_DATABASE].[dbo].[User]  
(  
    id INT NOT NULL UNIQUE,
    FullName VARCHAR(256) NOT NULL,
    Email VARCHAR(256) NOT NULL,
    Password NVARCHAR(32),
    Role VARCHAR NOT NULL  
);


ALTER TABLE [TEST_DATABASE].[dbo].[User] ALTER COLUMN userRole VARCHAR(256) NOT NULL

EXEC sp_rename 'User.userFullName', 'fullName'
EXEC sp_rename 'User.userEmail', 'email'
EXEC sp_rename 'User.userPassword', 'password'
EXEC sp_rename 'User.userRole', 'role'