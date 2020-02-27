/*
 *
 *
Create table tblAccounts 
(
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Username NVARCHAR(100) NOT NULL,
	DisplayName NVARCHAR(100) NOT NULL,
	[Password] NVARCHAR(1000) NOT NULL,
	[Type] INT NOT NULL
)

Create table tblTables 
(
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[Status] INT NOT NULL DEFAULT 0
)

Create table tblCategory
(
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL
)

Create table tblFoods
(
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	CategoryID INT NOT NULL,
	Price FLOAT NOT NULL

	Foreign key(CategoryID) References tblCategory(ID)
)

Create table tblBills
(
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CheckInDate DATE NOT NULL DEFAULT GetDate(),
	CheckOutDate DATE NOT NULL DEFAULT GetDate(),
	TableID INT NOT NULL,
	Discount INT NOT NULL DEFAULT 0,
	TotalPrice Float NOT NULL

	Foreign key(TableID) References tblTables(ID)
)

Create table tblBillInfo 
(
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BillID INT NOT NULL,
	FoodID INT NOT NULL,
	[Count] INT NOT NULL

	Foreign key(BillID) References tblBills(ID),
	Foreign key(FoodID) References tblFoods(ID)
)
*
*
*/