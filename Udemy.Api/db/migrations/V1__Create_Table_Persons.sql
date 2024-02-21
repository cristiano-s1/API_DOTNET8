CREATE TABLE Persons (
    Id int IDENTITY(1,1) PRIMARY KEY,
    [Address] varchar(100) NOT NULL,  
    FirstName varchar(80) NOT NULL,
    LastName varchar(80) NOT NULL,
    Gender varchar(30) NOT NULL,
    [Enabled] BIT,
    CreateDate datetime NULL,
    UpdateDate datetime NULL
);