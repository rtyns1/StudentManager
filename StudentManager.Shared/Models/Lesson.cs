namespace StudentManager.Shared.Models
{
    public class Lesson
    {
        /*
         * A scheduled teaching session. What does it need/
         * Which student? StudentId,
         * which subject? SubjectId
         * when does it start? -- SchediledStart - date, time
         * How long was the session? DurationMinutes
         * What is the topic of this specific lesson? --Topic
         * Status? --- scheduled, completed, cancelled etc etc-- useful for calender
         * 
         */
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string? Topic { get; set; }
        public DateTime ScheduledStart { get; set; }
        public LessonStatus Status { get; set; }
        public TimeSpan Duration { get; set; } // alternative is int DurationMinutes


       
    }
}
