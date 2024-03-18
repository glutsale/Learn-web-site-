using GlutLearn.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.ViewModels
{
    public class LessonInfoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Homework { get; set; }
        public string? VideoLink { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<Course> CourseList { get; set; }
        public IFormFile VideoFile { get; set; }



        public LessonInfoViewModel()
        {

        }

        public LessonInfoViewModel(Lesson model)
        {


            Id = model.Id;
            Title = model.Title;
            Homework = model.Homework;
            VideoLink = model.VideoLink;
            CourseId = model.CourseId;
            Course = model.Course;
    }

        public LessonInfoViewModel(List<Course> courseList)
        {
            CourseList = courseList;
        }
        public Lesson ConvertViewModel(Lesson model)
        {
            return new Lesson
            {
            Id = model.Id,
            Title = model.Title,
            Homework = model.Homework,
            VideoLink = model.VideoLink,
            CourseId = model.CourseId,
            Course = model.Course

        };
        }
    }
}
