using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.API.Dtos.Dentists;

public class UpdateDentistDto
{
    [Required] 
    [MaxLength(250)] 
    public required string Name { get; set; }

    [Required]
    [StringLength(254)]
    [EmailAddress]
    public required string Email { get; set; }
}