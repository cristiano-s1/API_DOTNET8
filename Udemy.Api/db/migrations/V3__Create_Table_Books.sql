CREATE TABLE Books (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Author varchar(200) NOT NULL,
    LaunchDate datetime NOT NULL,
    Price decimal(18,2) NOT NULL,
    Title varchar(100) NOT NULL,
    CreateDate datetime NULL,
    UpdateDate datetime NULL
)
