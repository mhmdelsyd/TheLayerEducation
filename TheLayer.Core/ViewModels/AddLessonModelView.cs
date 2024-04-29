using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.ViewModels
{
    public class AddLessonModelView
    {
        [Required]
        public string LessonName { get; set; }

        [Url]
        public string VideoUrl { get; set; }

        [Url]
        public string PdfUrl { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
