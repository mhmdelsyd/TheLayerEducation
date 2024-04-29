using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.ViewModels
{
    public class LessonModelView
    {
        public Guid Id { get; set; }

        [Required]
        public string? LessonName { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
