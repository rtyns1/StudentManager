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

-- The JSON format in appsettings.Development,Json and appsettings.Json are very important, any erro form there and the connection string wont be read properly and it will bring errors.
--->Now, it is standard and there are standard patterns in how to use appDbContext.cs and how to use connections trings and wahtnots, these are fundamentals.
-- So they are things that can be understood over time, patterns.

Now, the Database is set up.
-i have - SQL server DB with students and subjects tables created via NEsureCreated().
-AN AppDbContext class that connects your C# code to that database.
-Model classes, Student and Subject that represent rowsin those tables.
I will now need to allow the outside world to interact with the data. And this will be done through our Blazor Frontend.


**20th, May 2026**
**How will the blazor frontend talk to the backend?**
By using controllers.
A cnotroller is the translator: it receives HTTP requests, uses AppDbContext to run SQL queries via LINQ, and treturns the results as HTTP resposnes useally in Json.
Without a controller--frontend cnt talk to the database.

So, i need to create controllers.

-A controller is a translator btwn basically the frontend and the backend. We could say that.
- I need to create the StudentConctoller, In my StudentManager.API project, create a folder names Controllers.
- This is a naming conention that ASP.NET Core automatically finds contollers inside this foler, or anyother folder that inherits from ControllerBase and has ApiController attribute.
- Now, tbh i hve limited idea of what any of this mean sbut i will undestand once i actually start writing the code.
- So, right now i need to learn how to implement controllers, how the code could look like, and how eveerything works. And then  i will now write the full code inside.


-- Now, ive made the StudentController, now i need to test it. How do u test a controller? susing Swagger. I have no iea what swagger is. Also, what should the controller control? 

 ## ** 22nd Friday 2026**
Its been a long time since i coded a huge amount, but today hopefully i make huge orogress. Work and life obligations man.

Okay so so far, i have understood controllers, i understand why we need them but that doestn mean im yet cofortable writing them 100%, but the more i write the more i learn.
So, now i need to test StudentController--- so now that i have the minimum code for StudentController, i can test it and see that it actually works by buildin the Blazor frontedn for students.
This way, i can see what im doin step by step.
Buuldin the Blazor frontend fir students will allow me to:
-See my students in a web pagge.
-Add new students via a form.
-This gives me a complete working feature end to end, i can see the frontend and backedn working completely.
- but then, i need to first understand how many, and which controllers i need. SO, there is a list:
- StudentController  -- manage students
- SubjectController -- manage subjects-- GET, POST as minimum endpoints
- FilesController --Upload/Download Files--POST, GET as minimum endpoints
- LessonsController -- Schedule lessons--GET,POST, PUT as minimum endpoints
- LessonLogsController -- Record lesson cmopletion -- GET/api/lessonlogs/student/{studentId} as minnmum endpoint.

Preferably i should write all these minimum working versions in the next 1hr or 2hrs.
Its currently 5AM on Friday, i might not be too busy at work so ttoday i will aim to write alot of code, 5 days woth of code.
Also, i need to complete this as fast as possible im meeting with Adam soon and i need to be ready and prepared.

So, now how do i write blazor syntax? what even is blazor? bcz so far ive seen that C# has lots of things -- ive seen Razor pages before. but not blazor.

-->So i think its best to explore the differences btwn Razor pages, blazor, and MAUI and Blazor Hybrid at another time, as well as exploring where they are best used. For now, let me continue here.

### Intro to Blazor, testing StudentController, and basic structure of a blazor component.

-Blazor components are .razor files,, meaning they mix HTML and C#. I dont know shit abou tHTML, CSS or javascript.

```Csharp  // how to make all this look ike code in md?


@page "/example"          <!-- URL where this page is accessible -->
@inject HttpClient Http   <!-- Inject a service (like HttpClient) -->

<h3>Title</h3>            <!-- HTML markup -->

@* C# code block *@
@code {
    private string message = "Hello";

    protected override void OnInitialized()
    {
        // Runs when component loads
        message = "Loaded!";
    }
}
```

--------------------------------------------------------------------
->Now, i have made a few changes to the code. Signficant ones that need to be documented, and even checkpointed on my git commits.
I added a shared project--Razor Class Library.
AA shared project is a Razor Class Library (RCL) -- a special type of .NET project that:
-Contains reusable UI components(.razor) files, C# models , DTOs and sometiems static assets CSS Js
- Can be referenced by MULTIPLE FRONTEND PROJECTS --(blazor WebAssembly, Blazor Server, .NET MAUI Blazor etce tc
- IT does not have an entry point like program.cs, it is just a library that holds thins that are sharedthat are to be referenced, that is how they are accessed.
- Produces a .dll that other projects use.

It is like a shared toolbox::
- My API project is not allowed to reference the Shared project except for models/DTOS- but careful)
- My frontend, whether its web, desktop or mobile all reference the shared project to get the same UI and data contracts.

Best use cases for an RCL:
- When you have or want to implement multiple frontends, like web, mobile and desktop that should look and behavve identically.
- Why you want to maintain UO components in one place an reuse them,
- when you want to share C# models between the API and clients to avoid duplication.

**Now, why did i make the decision to include the Shared project?**
- I wanted both web and desktop. The backend is what stays contant but the frontend ca change in so many ways.
- So i will have 2 frontends: webapp-Blazor WebAssembly and a desktop app --MAUI Blazor.
- Without a shared project, you would have to duplicate every .razor file, every model class, every DTO - once in the web project and again in the desktop project.
- Any change to a page would need to be made in 2 places - error prone and time wasteful.

By introducing the shared project:
- All UI components are writtne once.
- Both web and desktop project eference the came components,
- Changes in the shared project automatically appear in both frontends when you rebuild

Now, waht changed? 

Before,i only had 2 projects -- StudentManager.API and StudentManager.BlazorClient. The blazor client contained all the UI.
Now, with the StudentManager.Shared, it holds:
-- All of .razor pages
-- All code relating to layout, nav menu
-- All models/ data containers
-- All DTOs


So, now what i need to add is the following  controllers::
-SubjectController, FilesController, LessonsController, and LessonLogsController.

- A DTO is a simple C# class that carries data btwn the API and client, but only the data you need at the moment.
- I will decide later if i am to separate DTOs.
- SO, **WHAT IS AN API ENDPOINT?**
- An API endpoint is simply a specific URL where one computer program asks another program to send back data or perfomr a task.
- Think of an API as a restaurant menu. The endpoint is a specific item you order like burger, or sode -- and the request tells the server exactly waht i want.

- So,a program uses a specific URL to reach an endpoint.
- Every endpoint expects you to include a methpd.  snigle word telling what u want to do with the URL::
- GET -- Give me this information.
- POST -- Create something new
- PUT/PATCH -- Update something that already exists.
- DELETE -- Remove something.

Without endpoints,2 apps wouldnt know how to talk to each other. Endpoints create clearly labeled doors to specific services so your app can seamlessly fetch data of execute commands
without needing to code those features from scratch.

Okay so the next controlle rim writing is for SubjectController. And its minimum endpoints will be ::
GET/ api/ subjectx-- to lust all the subjects/
POST / api/subjects-- to create a new subject.

***[RECAP BEFORE COMMIT]

WHAT DONE:: Written the student controller, the subject controller, understood fundamentals, and the API runs on swagger.
- I moved the .razor files out of the Shared, i deleted the layout-- it was causing build errors, i will add them if i need them later on.
- API project references shared project, also fixed namespaces issues.
- I used SQL server LocalDb with connection string in appsettins,json.
- used EnsureCreated() in program,cs of API to create the Database on app start.
- The tables i currnlty have are based on the data containers from Students and subjects, late ri will add more.
- For the models , i have:
- Student (Id, Name, Email, CreatedAt)
- Subject (Id, Name, Description)
- AppDbContext with DbSet<Student> students, and DbSet<Subject> subjects
- Constructor with DbContextOptions<AppDbContext>
- The controllers for StudentController and SubjectController are all written GETALL , POST create
- For the swagger set up, i installed Swashbuckle.AspNetCore (version 6.5.0).
- Added AddSwaggerGen() and UseSwagger(), UseSwaggerUI() in Program.cs.
- Fixed missing using Microsoft.AspNetCore.Builder; and ensured package was correctly referenced.
- Encoutered many errors, from build errros o InvalidOperationException errrs.
- Next time, i will be more detailed.

**WHWAT TO AIM FOR NEXT TIME? MINIMUM**
- Prepare models and DbContext -- Lesson, LessonLog, FileEntry models added, DbSet properties added, DB recreated.
- Write LessonController -- 	GET /api/lessons, POST /api/lessons – test with Swagger.
- Write LessonLogsController --GET /api/lessonlogs/student/{studentId}, POST /api/lessonlogs – test with Swagger.
- Write FilesController -- 	POST /api/files/upload (saves file to wwwroot/uploads, stores metadata) – test with Postman or Swagger (file upload in Swagger requires a bit of setup – I’ll show you).
- Add email scheduling endpoint basic -- EmailsController with POST /api/emails/schedule – stores email in ScheduledEmail table (no Hangfire yet – just save to DB).
- Test all endpoints, fix bugs, commit.

**NEXT TIME::25TH May 2026, Monday at 11pm**
-----> So, first start by writing the Lesson model class. It is a data container.
Lesson model and controller, written, reflects on swagger test.
LessonLogger written, controller and models written, reflects on Swagger test.
Works, executes, but says Failed to fetch. FIXABLE.
-- WHAT TO WORK ON NEXT:::
----> GET endpoint by studentId for logs-- undesrtand and write it.
-----> Better documentation.
-----> How to test with swagger.
-----> Lesson and LessonLog relationship including foreignkeys and DB connections, as well as some commmon sql server syntax and code.
-----> Write Filecotnroller, emailcontroller, and make it scehdule and send an email, 
-----> i was tired, not at 100% energy and efficiency today. tmrw will be better.