﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
    public class EmployeeModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "ID Number must be 11 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ID Number must contain only numbers.")]
        public string IdentifyNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public string? PhoneNumber { get; set; }

        [ForeignKey("GenderModel")]
        public Guid GenderId { get; set; }
    }
}
