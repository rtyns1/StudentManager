This is where i wil be dumping my mind, like a rough work place. Also, will be showing where im at on specific days.
## Current status on MAY 17TH 2026, SUNDAY: phase 0 - Documentation and setup complete. Building MVP.

-- so, we need a working mvp to give us direction.
-- so ,today i need to complete a few tasks:
1. Add a student.
2. Add a subject.
3. Upload a file to a subject.--Notes, questions, CATS, exams etc
4.Schedule a lesson --pick student, subject, date, time, aduration
5.View calender with scheduled lessons.
6.Mark lesson as complete and add a log on what taught, progress and homework.
7.View Student record - list of lessons with logs
8.Schedule an Email to a student with a attachment from a subject section.
9.Automated email sending via Hangfire-- test with a real email.

-- other features like dashboard, tags, searc, delete edit etc etc will be made post mvp.

-- WE need to take a Database first approach.
-- Set up DbContext.cs and connection string
--  A connection string is a string that tells the Entity Framework how to find the database server, which database to use, and how to authenticate.
-- We willl use a SQL Server LocalDB---> so we will need a localDB connection string.

-- An example of a connection string connecting to LocalDB::: server = (localdb)\mssqllocaldb;Database=StudentManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true
-Server=(localdb)\mssqllocaldb – points to the LocalDB instance.

-Database=StudentManagerDb – the name of the database (will be created automatically).

-Trusted_Connection=True – uses your Windows credentials (no separate username/password).

-MultipleActiveResultSets=true – allows multiple active result sets (useful for some queries).

--Without a connection string, EF, ENtity framework wont know where to put the tables.
-- The connection string is stored in appsettings.Json of the API project. NOT IN CODE because it is a setting and settings are meant to be configured.
### **What is Entity Framework?** 
-- open source Object Relational Mapper(ORM) fr.NET. It allows developers to work with databases using C# objects rater than writing raw SQL.
It acts a s a bridge between your application code and the database.

-->Some key concepts of Entity framework::::
*Abstraction:: EF eliminates the need for most data access plumbing code by automatically translating your objct oriented actions int database specific commands.
*Strongly Typed Queries:: Instead of writing SQL strings, you use LINQ which allows the compilor to catch erros at compile time rather than at runtmie.

**Core components**

*DbContext: The primary class that represents a session with the database, used to query and save data.
*DbSet: Represents a collection of a specific type of entity (like a "Students" table) in the database.
*Migrations: A tool to keep your database schema in sync with your C# code as you add or change fields.

**Development approaches**
*Code first approach -- where you write the C# classes first, and thenEF creates the daatabase for you.
* Databade first approach -- where you generate C# classes from an existing database.


--- We are going to use the Code first approach.
---MODELS ARE THE CLASSES THAT BECOME TABLES

**What is a model?**
-A plain C# class that represents a real world entity.
-Ech property becomes a column in the database table.
- EF core uses conventions to map the class name to a table name. pluralised by default
- Models are purely data containers-- no logic just properties.
- Then, appDbContext class is used to manage these models and their corresponding tables in the database.
- DbContext uses your models to talk to the database.
- Takes model-- figures out what SQL table that maps to 
- it lets you query, insert, update, delete using C# LINQ instead of writing raw SQL

**What does DbContext do?**
- Holds DbSet<T> properties = eac one is a gateway to a table.
- Manages the connection to the database using the connection string.
- Tracks changes to objects so it knows what to save.
- Create the dabase id it does not exist via EnsureCreated() or migrations.

**Whst is user-secret?