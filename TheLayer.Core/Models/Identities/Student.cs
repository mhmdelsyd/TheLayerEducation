using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLayer.Core.Models.Identities
{
    public class Student : Consumer
    {

        [Display(Name = "Subscribed Teachers")]
        public List<Teacher>? SubscribedTeachers { get; set; }
    }
}
