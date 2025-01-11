using System.Collections.Generic;

namespace ProjectB.Models
{
    public class LessonsViewModel
    {
        public IEnumerable<Lessons> LessonsList { get; set; }
        public Lessons NewLesson { get; set; } = new Lessons();
    }
}
