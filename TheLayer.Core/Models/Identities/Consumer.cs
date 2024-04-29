using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLayer.Core.Models.Identities
{
    public class Consumer : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[]? UserPhoto { get; set; }
    }
}
