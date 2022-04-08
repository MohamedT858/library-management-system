Create Database Library;

Create Table Publisher(
Pub_Id int NOT NULL Identity  CONSTRAINT PK_Pub_Id PRIMARY KEY,
Name Varchar(40) NOT NULL,
Phone varchar(40),
Address Varchar(100)
);

Create Table Librarian(
Lib_Id int NOT NULL Identity CONSTRAINT PK_Lib_Id PRIMARY KEY,
Name Varchar(40) NOT NULL,
Phone varchar(40),
Address Varchar(100)
);

Create Table Member(
Mem_Id int NOT NULL Identity CONSTRAINT PK_Mem_Id PRIMARY KEY,
Fname varchar(15),
Mname varchar(15),
Lname varchar(15),
Address varchar(60),
Age int,
Lib_Id int,
Constraint FK_Lib_Id Foreign Key (Lib_Id) References Librarian(Lib_Id)
);

Create Table Books(
Book_Id int NOT NULL Identity Primary Key,
Title varchar(50) NOT NULL,
Author varchar(50),
Price Money,
Return_day date,
CheckOut_day date,
Pub_Id int,
Mem_Id Int,
Constraint FK_Pub_Id Foreign Key (Pub_Id) References Publisher(Pub_Id),
Constraint FK_Mem_Id Foreign Key (Mem_Id) References Member(Mem_Id)
);


insert into Publisher(Name) 
Values('Adis International'),('Anchor Book Press'),('Autumn House Press'),('Beacon Publishing'),('BookPress Publishing'),
('Dover Publication'),('Inkwater Press'),('Inkwater Press'),('Little Book Press'),('Progress Publishers')

insert into Librarian(Name)
Values('Abdallah Ahmed'),('Mosatfa Hussain'),('Ahmed Mohamed'),('Hasan Eid')

insert into Books(Title,Author,Price,Pub_Id) 
Values('Five Minutes in China', 'LOUIE QUINN', 50.00, 1),
('The Gunpowder Magazine', 'IKE NICHOLLS', 69.00, 2),
('The Books of Moses and Sons', 'STEWART BAILEY', 42.0, 3),
('Morrison’s Pills Progress', 'BARNABY GEORGE', 44.99, 4),
('Jonah’s Account of the Whale', 'GARY PAYNE', 24.99, 5),
('Matthew’s Nursery Songs', 'CURTIS RAMSEY', 30.00, 6),
('Lady Godiva on the Horse', 'DONALD TINGEY', 49.99, 7),
('Commonplace Book of the Oldest Inhabitant', 'HARLEY INGRAM', 80.08, 9)

create View Currently_Borrowed AS
Select Book_Id 'Book ID',Title,Price,Books.Mem_Id'Borrower ID',
Member.Fname'First Name',Member.Lname'Last Name',CheckOut_day'Check Out date',Return_day'Return Date'
From Books Join Member On Member.Mem_Id = Books.Mem_Id ;

Create View Stored_Books As
select Book_Id'Book ID', Title, Author, Price, Publisher.Name'Publisher', Mem_Id 
From Books Join Publisher On Books.Pub_Id=Publisher.Pub_Id Where Mem_Id is NULL










