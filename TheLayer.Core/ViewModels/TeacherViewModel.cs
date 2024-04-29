using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLayer.Core.ViewModels
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string course { get; set; }
        public bool Subscribed { get; set; }
    }
}
