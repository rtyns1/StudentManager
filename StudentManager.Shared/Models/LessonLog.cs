namespace StudentManager.Shared.Models
{
    public class LessonLog
    {
        /*
         * A lessonlog is a record after th elesson is complete- not the scheduled event intself. It caprtures what actually hapened.
         * So, what should LessonLog contain? atleast for MVP?
         * 1. LessonId--- foreignkey to the Lessons table
         * 2.LessonLogId
         * 2. WhatWasTaught -- string -- contnt covered
         * 3.StudentProgressnotes--string--bservations lik estrenghts, weakenesses, and things to work on
         * 4.Homework/Assignment given --- string -- work given to student
         * 5.CompletionPercentage int -- 0-100--- how much of the planned lesson was completed?
         * 6. When was this log created? ---CompletedAt --- set to DateTime.UtcNow when the log is created.
         * 
         * we dont need StudentID bcz we can get it from the Lesson via the foreign key, and SubjectId for the same reason
         * Topic is already in Lesson.
         * int Id

             int LessonId

             string WhatWasTaught


             string StudentProgressNotes (nullable)

             string HomeworkGiven (nullable)

             int CompletionPercent

              DateTime CompletedAt
           */
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string? WhatWsTaught { get; set; }
        public string? StudentProgressNotes { get; set; }
        public string? AAssignmentGiven { get; set; }
        public int CompletionPercent { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
