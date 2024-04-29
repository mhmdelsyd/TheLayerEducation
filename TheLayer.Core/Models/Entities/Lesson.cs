using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Models.Identities;

namespace TheLayer.Core.Models.Entities
{
    [Table(nameof(Lesson))]
    public class Lesson
    {
        [Key]
        public Guid LessonId { get; set; }

        [Required]
        public string LessonName { get; set; }

        [Url]
        public string VideoUrl { get; set; }

        [Url]
        public string PdfUrl { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public Teacher Teacher { get; set; }
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
    }
}
