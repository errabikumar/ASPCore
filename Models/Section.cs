using System.ComponentModel.DataAnnotations;

namespace MVC_Task.Models
{
    public class Section
    {
        
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public string CourseId { get; set; }
    }
}
