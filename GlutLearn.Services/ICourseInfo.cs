using glutlearn.Utilities;
using GlutLearn.Models;
using GlutLearn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Services
{
    public interface ICourseInfo
    {
        PagedResult<CourseInfoViewModel> GetAll(int pageNumber, int pageSize);
        CourseInfoViewModel GetCourseById(int Id);
        void UpdateCourseInfo(CourseInfoViewModel vm);
        void InsertCourselInfo(CourseInfoViewModel vm); 
        void DeleteCourseInfo(int id); 
    }
}
