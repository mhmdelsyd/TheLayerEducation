using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Dtos;
using TheLayer.Core.Models.Entities;

namespace TheLayer.IServices.IApplicationServices
{
    public interface ICourseServices
    {
        ResponseModel<bool> AddCourse(string courseName);
        ResponseModel<bool> EditCourse(Guid courseId, string courseName);
        ResponseModel<IEnumerable<Course>> GetAllCourses();
        ResponseModel<IEnumerable<string>> GetAllCoursesNames();
    }
}
