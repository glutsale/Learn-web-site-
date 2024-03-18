using glutlearn.Utilities;
using GlutLearn.Models;
using GlutLearn.Repositories.Interfaces;
using GlutLearn.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Services
{
    public class LessonInfoService : ILessonInfo
    {
        private IUnitOfWork _unitOfWork;
        private VideoSender _videoSender;

        public LessonInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _videoSender = new VideoSender();
        }

        public List<Course> GetAllCourses()
        {
            return _unitOfWork.GenericRepository<Course>().GetAll().ToList();
        }

        public void DeleteLessonInfo(int id)
        {
            var model = _unitOfWork.GenericRepository<Lesson>().GetById(id);
            _unitOfWork.GenericRepository<Lesson>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<LessonInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            
            var vm = new LessonInfoViewModel();
            int totalCount = 0;
            List<LessonInfoViewModel> vmList = new List<LessonInfoViewModel> ();
            try {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Lesson>().GetAll(includeProperties:"Course")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Lesson>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }
            var result = new PagedResult<LessonInfoViewModel> {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }



        public LessonInfoViewModel GetLessonById(int LessonId)
        {
            var model = _unitOfWork.GenericRepository<Lesson>().GetById(LessonId);
            var vm = new LessonInfoViewModel(model);
            return vm;
        }


        public void InsertLessonInfo(LessonInfoViewModel vm)
        {
            var lesson = new Lesson
            {
                Id = vm.Id,
                Title = vm.Title,
                Homework = vm.Homework,
                VideoLink = vm.VideoLink,
                CourseId = vm.CourseId
            };

            _unitOfWork.GenericRepository<Lesson>().Add(lesson);
            _unitOfWork.Save();
        }

        public void UpdateLessonInfo(Lesson Lesson)
        {

        }

        public void UpdateLessonInfo(LessonInfoViewModel vm)
        {
            var ModelById = _unitOfWork.GenericRepository<Lesson>().GetById(vm.Id);
            ModelById.Title = vm.Title;
            ModelById.Homework = vm.Homework;
            ModelById.VideoLink = vm.VideoLink;
            _unitOfWork.GenericRepository<Lesson>().Update(ModelById);
            _unitOfWork.Save();

    }

        private List<LessonInfoViewModel> ConvertModelToViewModelList(List<Lesson> modelList)
        {
            return modelList.Select(x => new LessonInfoViewModel(x)).ToList();
        }

        public string UploadVideoAndGetLink(IFormFile videoFile, string title, string description, string[] tags)
        {
                try
                {
                    // Создание временного файла для загружаемого видео
                    var tempFilePath = Path.GetTempFileName();
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        videoFile.CopyTo(stream);
                    }

                    // Загрузка видео и получение ссылки на него
                    var videoLink = _videoSender.UploadVideoAndGetUrl(tempFilePath, title, description, tags);

                    // Удаление временного файла
                    File.Delete(tempFilePath);

                    return videoLink;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок загрузки видео
                    Console.WriteLine("Ошибка загрузки видео: " + ex.Message);
                    return null;
                }
            }
        }
    }
