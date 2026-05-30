# Changelog

-- Use semantic verison control::
v0.0.1 - 
v0.0.2--
then maybe v1.0.1 -- Working mvp. Now this is a new pahse, will eventually have v1.1.2 etc etc
either way, track in a format that is understandable and use tags. Tags will make my life easier 


## [Unreleased]
- Documentation setup
- Created solution with API + Blazor projects

## [v0.1.0] - Planned
- Student CRUD
- Subject management
- File upload for sections

# DETAILED VERSION HITORY AND PROGRESS TRACKING, NEAT PROGRESS TRACKING UNLIKE LEARNINGLOG.MD


# ***v0.3.0 – 2026-05-28 , Thursday(Backend MVP: Email Automation Complete)***

# Student Manager Project – Backend MVP Completion Log

## Current Status (as of this session)

The backend of the Student Manager application has reached a stable, functional state for the core MVP features. All essential database tables, models, API controllers, and automated email sending are working and tested via Swagger and manual verification.

---

## 1. Completed Features

### 1.1 Database and Models

- Database engine: SQL Server LocalDB (instance: `localhost\SQLEXPRESS`)
- Database name: `StudentManagerDb`
- Tables created automatically via `EnsureCreated()` in `Program.cs`
- All tables are defined through Entity Framework Core code‑first models.

Models are stored in the `StudentManager.Shared` project, organised into `Models/` and `Enums/` folders. The following models exist:

| Model | Purpose | Key Properties |
|-------|---------|----------------|
| `Student` | A person taking lessons | Id, Name, Email, CreatedAt |
| `Subject` | Academic subject (Math, English, etc.) | Id, Name, Description |
| `Lesson` | Scheduled teaching session | Id, StudentId, SubjectId, Topic, ScheduledStart, DurationMinutes, Status (enum) |
| `LessonLog` | Record after a lesson is completed | Id, LessonId, WhatWasTaught, StudentProgressNotes, HomeworkGiven, CompletionPercent, CompletedAt |
| `FileEntry` | Metadata for uploaded files | Id, Name, StoredFileName, UploadedAt, SubjectId (optional), FileType (enum), SectionType (enum) |
| `ScheduledEmail` | Email queued for future delivery | Id, StudentId, Subject, Body, AttachmentFileId, ScheduledAt, IsSent, SentAt |
| `StudentSubject` | Junction table for many‑to‑many relationship (not yet used) | Id, StudentId, SubjectId |

Enums created:
- `LessonStatus` (Scheduled, Completed, Cancelled)
- `FileTypeEnum` (Document, Image, Video, Other)
- `SectionTypeEnum` (Notes, Questions, CATs, Exams)

### 1.2 API Controllers

All controllers are located in `StudentManager.API/Controllers/`. Each controller is decorated with `[ApiController]` and `[Route("api/[controller]")]`. They inject `AppDbContext` via constructor.

| Controller | Endpoints | Tested via Swagger |
|------------|-----------|---------------------|
| `StudentsController` | GET `/api/Student`<br>POST `/api/Student` | Yes |
| `SubjectsController` | GET `/api/Subject`<br>POST `/api/Subject` | Yes |
| `LessonsController` | GET `/api/Lesson`<br>POST `/api/Lesson` | Yes |
| `LessonLogsController` | GET `/api/LessonLogs`<br>POST `/api/LessonLogs` | Yes |
| `FilesController` | POST `/api/Files/upload` | Partial (requires Postman) |
| `ScheduledEmailController` | POST `/api/ScheduledEmail` | Yes |

### 1.3 Email Automation (Hangfire + MailKit)

- **EmailService** (`Services/EmailService.cs`): Uses MailKit to send emails via Gmail SMTP (port 587, TLS). Reads sender email and app password from `appsettings.Development.json`.
- **EmailJob** (`Services/EmailJob.cs`): A Hangfire background job that runs every minute. It queries `ScheduledEmails` where `IsSent == false` and `ScheduledAt <= DateTime.UtcNow`. For each pending email, it:
  - Loads the associated student.
  - If an `AttachmentFileId` is provided, retrieves the `FileEntry` and builds a full file path.
  - Calls `EmailService` to send the email.
  - Marks the email as sent (`IsSent = true`, `SentAt = now`).
- Hangfire is configured in `Program.cs` with SQL Server storage and a recurring job using the cron expression `* * * * *` (every minute).

### 1.4 Configuration and Middleware

- `Program.cs` includes:
  - DbContext registration with SQL Server connection string.
  - Controllers and Swagger services.
  - CORS policy to allow requests from the future Blazor frontend (e.g., `https://localhost:5001`).
  - Hangfire server and dashboard.
  - `EnsureCreated()` for database auto‑creation (development only).
- `appsettings.json` contains the `ConnectionStrings` section.
- `appsettings.Development.json` contains `EmailSettings` (sender email and app password) – never committed to git.

### 1.5 Testing

- Swagger UI is available at `/swagger`. All GET and POST endpoints have been tested manually with valid and invalid inputs.
- File upload was tested using Postman (Swagger does not support file upload easily).
- Hangfire dashboard is available at `/hangfire` – shows successful job executions.
- End‑to‑end email test: scheduled an email via Swagger, waited 2 minutes, received the email at the student’s address.

---

## 2. Key Technical Decisions and Lessons Learned

### 2.1 Separation of Concerns
- The API project handles HTTP and database access.
- The Shared project holds only models and enums, used by both API and frontend.
- This allows future frontends (Blazor WebAssembly, MAUI, mobile) to reuse the same data contracts.

### 2.2 Entity Framework Core – Code First
- Models are written in C#; EF Core generates the database schema.
- `EnsureCreated()` is sufficient for development; migrations will be added later.
- Always set primary keys (`Id` or `ClassnameId`). Without it, EF Core throws an error.

### 2.3 Controllers and Dependency Injection
- Each controller receives `AppDbContext` through its constructor. ASP.NET Core’s DI container provides it.
- `[FromBody]` is used for POST data. Validation is done manually (e.g., check for null, future date).

### 2.4 Email Sending with MailKit
- SMTP server: `smtp.gmail.com`, port 587, `StartTls`.
- Authentication requires an App Password (not regular password). Generate it from Google Account settings.
- Attachments are added using `BodyBuilder.Attachments`.

### 2.5 Hangfire
- Requires a persistent storage (SQL Server). Use `Hangfire.SqlServer`.
- Recurring jobs are defined with cron expressions.
- The job class must be registered as a scoped service so it can inject `AppDbContext` and `EmailService`.

### 2.6 CORS
- By default, browsers block requests from different origins (e.g., Blazor frontend on port 5001 to API on port 5000).
- Solution: Add a CORS policy in `Program.cs` with `WithOrigins` specifying the frontend URL.

### 2.7 Common Errors and Fixes
- `InvalidOperationException: The entity type 'FileEntry' requires a primary key` → Added `public int Id { get; set; }`.
- `'AttachmentFileId' is string, cannot use >` → Changed model property to `int?`.
- `UseSwaggerUI` red underline → Installed `Swashbuckle.AspNetCore` and added `using Swashbuckle.AspNetCore.Builder`.
- `Failed to fetch` in Swagger → Added CORS middleware.
- `Cannot apply > operator to string` → Ensured `AttachmentFileId` is nullable int and used `.HasValue` / `.Value` correctly.

---

## 3. What Has Not Been Done Yet (Post‑MVP)

The following features are planned but not yet implemented:

- Student‑subject many‑to‑many linking (junction table `StudentSubject` is ready but no UI or logic).
- File upload UI in Blazor (currently only via Postman).
- Calendar view (FullCalendar integration) to display lessons.
- Lesson scheduling and logging UI.
- Student record page showing all lessons, logs, and pending work.
- Dashboard with statistics.
- Search and filter functionality.
- Student response mechanism (comments, submission marking).
- Authentication / login.

These will be built in future sessions after the basic email automation is fully integrated with a working frontend.

---

## 4. Next Steps (Immediate Focus)

The immediate goal is to **integrate the existing backend with a functional Blazor frontend** to demonstrate the complete workflow:

1. **List students** – Blazor page that calls `GET /api/Student` and displays a table.
2. **Schedule an email** – Blazor form with:
   - Dropdown for students (fetched from API).
   - File picker (to upload a file, call `POST /api/Files/upload`, and store the returned file ID).
   - Subject, body, and datetime picker.
   - On submit, call `POST /api/ScheduledEmail` with the data.
3. **Test end‑to‑end** – upload a file via the Blazor form, schedule an email to yourself, wait and verify delivery.

After the frontend is working, we will add:
- Lesson scheduling UI.
- Calendar view.
- Lesson log UI.

---

## 5. Summary of Working Code (Key Files)

### `Program.cs` (condensed)

- Registers DbContext, Controllers, Swagger, CORS, Hangfire, EmailService, EmailJob.
- Builds the app, enables Swagger UI in development, adds Hangfire dashboard.
- Adds recurring job for email sending.
- Calls `EnsureCreated()` for database.
- Runs the application.

### `EmailService.cs`

- Constructor accepts `IConfiguration`.
- `SendEmailAsync` creates a `MimeMessage`, adds body and optional attachment, connects to Gmail SMTP, authenticates, sends.

### `EmailJob.cs`

- Constructor accepts `AppDbContext` and `EmailService`.
- `SendPendingEmailsAsync` queries unsent emails, loops through, sends each using `EmailService`, marks as sent.

### `ScheduledEmailController.cs`

- `[HttpPost]` creates endpoint `/api/ScheduledEmail`.
- Validates `StudentId > 0` and `ScheduledAt > DateTime.UtcNow`, sets `IsSent = false`, saves to database.

### `FilesController.cs` (partial)

- `[HttpPost("upload")]` accepts `IFormFile`, saves to `wwwroot/uploads/`, creates a `FileEntry` record, returns `fileId`.

---

## 6. Documentation and Logging

This learning log is stored in the `docs/` folder of the repository. All commits are tagged with semantic versions (e.g., `v0.1.0`, `v0.2.0`). The next commit will be `v0.3.0` marking the completion of backend email automation.
