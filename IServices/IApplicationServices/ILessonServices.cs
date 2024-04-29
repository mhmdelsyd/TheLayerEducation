using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Dtos;
using TheLayer.Core.Models.Entities;
using TheLayer.Core.ViewModels;

namespace TheLayer.IServices.IApplicationServices
{
    public interface ILessonServices
    {
        ResponseModel<bool> EditLesson(EditLessonModelView model);
        ResponseModel<bool> AddLesson(AddLessonModelView model);
        ResponseModel<bool> DeleteLesson(Guid LessonId);
        ResponseModel<Lesson> GetLessonById(Guid LessonId);
        ResponseModel<IEnumerable<Lesson>> GetAllLessonsByTeacherId(string TeacherId);
        ResponseModel<IEnumerable<Lesson>> GetAllLessonsByCourseId(Guid CourseId);
        ResponseModel<IEnumerable<Lesson>> GetAllLessonsByFilter(params Func<Lesson, bool>[] filters);
    }
}
