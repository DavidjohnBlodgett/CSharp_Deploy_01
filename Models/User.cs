using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharp_belt_exam_project_two.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int userid { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<Like> likes { get; set; }

        public List<Idea> posts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; }

        public User() {
            likes = new List<Like>();
            posts = new List<Idea>();
        }

    }
}