using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Helpers.Constants;
using TheLayer.Core.Models.Identities;
using TheLayer.IServices.AccountsServices;

namespace TheLayer.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<Admin> _adminManager;
        private readonly UserManager<Teacher> _teacherManager;
        private readonly UserManager<Student> _studentManager;

        public AccountServices(UserManager<Admin> AdminManager, UserManager<Teacher> TeacherManager, UserManager<Student> StudentManager)
        {
            _adminManager = AdminManager;
            _teacherManager = TeacherManager;
            _studentManager = StudentManager;
        }
        public bool IsUserInRole(Roles role, ClaimsPrincipal User)
        {
            return true;
        }
    }
}
