using glutlearn.Utilities;
using GlutLearn.Models;
using GlutLearn.Repositories.Interfaces;
using GlutLearn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Services
{
    public class CourseInfoService : ICourseInfo
    {
        private IUnitOfWork _unitOfWork;

        public CourseInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteCourseInfo(int id)
        {
            var model = _unitOfWork.GenericRepository<Course>().GetById(id);
            _unitOfWork.GenericRepository<Course>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<CourseInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            
            var vm = new CourseInfoViewModel();
            int totalCount = 0;
            List<CourseInfoViewModel> vmList = new List<CourseInfoViewModel> ();
            try {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Course>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Course>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }
            var result = new PagedResult<CourseInfoViewModel> {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }



        public CourseInfoViewModel GetCourseById(int CourseId)
        {
            var model = _unitOfWork.GenericRepository<Course>().GetById(CourseId);
            var vm = new CourseInfoViewModel(model);
            return vm;
        }


        public void InsertCourselInfo(CourseInfoViewModel vm)
        {
            var course = new Course
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                Lessons = vm.Lessons,
                TeacherId = vm.TeacherId
            };

            _unitOfWork.GenericRepository<Course>().Add(course);
            _unitOfWork.Save();
        }


        public void UpdateCourseInfo(Course Course)
        {

        }

        public void UpdateCourseInfo(CourseInfoViewModel vm)
        {
            var ModelById = _unitOfWork.GenericRepository<Course>().GetById(vm.Id);
            ModelById.Name = vm.Name;
            ModelById.Description = vm.Description;
            ModelById.Lessons = vm.Lessons;
            _unitOfWork.GenericRepository<Course>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<CourseInfoViewModel> ConvertModelToViewModelList(List<Course> modelList)
        {
            return modelList.Select(x => new CourseInfoViewModel(x)).ToList();
        }

    }
}
