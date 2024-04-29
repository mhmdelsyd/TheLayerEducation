using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Models.Entities;

namespace TheLayer.Core.Models.Identities
{
    public class Teacher : Consumer
    {
        public List<Lesson>? Lessons { get; set; }
        public List<Student>? SubscribedStudents { get; set; }
        public Course? Course { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
    }
}
