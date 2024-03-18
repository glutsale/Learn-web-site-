using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Homework { get; set; }
        public string? VideoLink { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
