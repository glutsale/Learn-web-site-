using glutlearn.Utilities;
using GlutLearn.Models;
using GlutLearn.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Services
{
    public interface ILessonInfo
    {
        PagedResult<LessonInfoViewModel> GetAll(int pageNumber, int pageSize);
        LessonInfoViewModel GetLessonById(int Id);
        void UpdateLessonInfo(LessonInfoViewModel vm);
        void InsertLessonInfo(LessonInfoViewModel vm); 
        void DeleteLessonInfo(int id);
        List<Course> GetAllCourses();
        string UploadVideoAndGetLink(IFormFile videoFile, string title, string description, string[] tags);
    }
}
