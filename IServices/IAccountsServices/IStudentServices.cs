using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Dtos;
using TheLayer.Core.Helpers.Constants;
using TheLayer.Core.ViewModels;

namespace TheLayer.IServices.IAccountsServices
{
    public interface IStudentServices
    {
        ResponseModel<bool> LoginAsTeacher(LoginModelView model, List<Claim> claims = null);
        ResponseModel<bool> RegisterTeacher(RegisterModelView model, List<Claim> claims = null);
        ResponseModel<bool> SignOut();
        ResponseModel<bool> EditTeacherDetails(EditModelView model);
        ResponseModel<bool> VerifyChangeStudentPassword(string studentEmail);
        ResponseModel<bool> ChangeStudentPassword(string studentEmail, string token, string newPassword);
        ResponseModel<SubscribeState> SubscribeWithTeacher(string TeacherId);
    }
}
