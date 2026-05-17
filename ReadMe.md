# StudentManager – Automated Student Work Tracker

## Purpose
A system for teachers to manage students, subjects, files (notes/questions/CATs/exams), schedule lessons, log daily progress,
and send automated emails with attachments.
Built to help me manage tuition students and track their progress efficiently.

## Tech Stack
- Backend: ASP.NET Core Web API (C#)
- Frontend: Blazor WebAssembly (separate project) -- same solution
- Database: SQL Server (LocalDB for development) -- Entity Framework Core.
- Authentication: ASP.NET core identity.
- File storage: Local file system for development, with an abstraction layer for future cloud storage.
- Scheduling: Hangfire
- Logging: Serilog
- Background Tasks:Hangfire
- Email: MailKit + SMTP


