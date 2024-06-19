using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalPage.Application.Models
{
    public class SkillsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Skill { get; set; }
        public required string Qualification { get; set; }
        public required string Description { get; set; }

    }
}
