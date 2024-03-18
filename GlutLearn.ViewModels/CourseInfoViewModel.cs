using GlutLearn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.ViewModels
{
    public class CourseInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int TeacherId { get; set; }
        public virtual ApplicationUser Teacher { get; set; }

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();


        public CourseInfoViewModel()
        {

        }

        public CourseInfoViewModel(Course model)
        {


            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Lessons = model.Lessons;
            TeacherId = model.TeacherId;
    }

        public Course ConvertViewModel(Course model)
        {
            return new Course
            {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Lessons = model.Lessons,
            TeacherId = model.TeacherId
        };
        }
    }
}
