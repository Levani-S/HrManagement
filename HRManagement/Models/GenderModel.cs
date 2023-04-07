using System.ComponentModel.DataAnnotations;

namespace HRManagement.Models
{
    public class GenderModel
    {
        [Key]
        public Guid GenderId { get; set; }

        public string GenderName { get; set; }
    }
}
