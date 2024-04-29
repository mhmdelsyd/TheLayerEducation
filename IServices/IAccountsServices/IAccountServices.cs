using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Helpers.Constants;

namespace TheLayer.IServices.AccountsServices
{
    public interface IAccountServices
    {
        bool IsUserInRole(Roles role, ClaimsPrincipal User);
    }
}
