using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Dtos;
using TheLayer.Core.Models.Identities;
using TheLayer.Core.ViewModels;

namespace TheLayer.IServices.IAccountsServices
{
    public interface ITeacherServices
    {
        ResponseModel<bool> LoginAsTeacher(LoginModelView model, List<Claim> claims = null);
        ResponseModel<bool> RegisterTeacher(RegisterModelView model, List<Claim> claims = null);
        ResponseModel<bool> SignOut();
        ResponseModel<bool> EditTeacherDetails(EditModelView model);
        ResponseModel<IEnumerable<ReturnedTeacherModelView>> GetAllTeachersForAdmin();
        ResponseModel<IEnumerable<ReturnedTeacherModelView>> GetAllTeachersForStudents();
        ResponseModel<IEnumerable<TeacherViewModel>> GetAllSubscribedTeachersByStudentName(string studentName);
        ResponseModel<IEnumerable<TeacherViewModel>> GetAllTeachers();
        ResponseModel<IEnumerable<TeacherViewModel>> GetAllLessonsByFilter(params Func<Teacher, bool>[] filters);
        ResponseModel<IEnumerable<TeacherViewModel>> GetTopTeachersByNumberOfLessons(int TeachersNum);
        ResponseModel<IEnumerable<TeacherViewModel>> GetTopTeachersByNumberOfSubscripedStudents(int TeachersNum);
    }
}
