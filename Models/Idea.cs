using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharp_belt_exam_project_two.Models {
    public class Idea : BaseEntity {
        [Key]
        public int ideaid { get; set; }
        public int userid { get; set; }
        public User user { get; set; }
        public string content { get; set; }
        public List<Like> likes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; }

        public Idea () {
            likes = new List<Like>();
        }
    }
}