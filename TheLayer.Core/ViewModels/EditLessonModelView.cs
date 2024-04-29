using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.ViewModels
{
    public class EditLessonModelView
    {
        public Guid Id { get; set; }

        [Required]
        public string? LessonName { get; set; }

        [Url]
        public string? VideoUrl { get; set; }

        [Url]
        public string? PdfUrl { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
