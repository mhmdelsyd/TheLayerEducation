using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Dtos;
using TheLayer.Core.ViewModels;

namespace TheLayer.IServices.IAccountsServices
{
    public interface IAdminServices
    {
        ResponseModel<bool> LoginAsAdmin(LoginModelView model, List<Claim> claims = null);
        ResponseModel<bool> RegisterAdmin(RegisterModelView model, List<Claim> claims = null);
        ResponseModel<bool> SignOut();
        ResponseModel<ConfirmationModelView> CreateTeacher(CreateTeacherModelView model);
        ResponseModel<bool> EditAdminDetails(EditModelView model);
        ResponseModel<ConfirmationModelView> ResetTeacherPassword(ResetTeacherPasswordModelView model);
        ResponseModel<ConfirmationModelView> ChangeAdminPassword(ResetAdminPasswordModelView model);
    }
}
