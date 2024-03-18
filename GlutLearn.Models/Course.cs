using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int TeacherId { get; set; }
        public virtual ApplicationUser Teacher { get; set; }

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    }


}
