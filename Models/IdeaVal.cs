using System.ComponentModel.DataAnnotations;

namespace CSharp_belt_exam_project_two.Models {
    public class IdeaVal : BaseEntity {
        [Required(ErrorMessage = "Idea must be 2 characters in length!")]
        [MinLength(2)]
        public string content { get; set; }
    }
}