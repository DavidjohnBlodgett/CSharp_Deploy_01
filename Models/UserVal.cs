using System.ComponentModel.DataAnnotations;

namespace CSharp_belt_exam_project_two.Models
{
    public class UserVal : BaseEntity
    {
        [Required(ErrorMessage = "Names must be 4 characters in length and only contain letters!")]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Alias must be 2 characters in length and only contain letters!")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        public string alias { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        public string conf_pass { get; set; }
    }
}