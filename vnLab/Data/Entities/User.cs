using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace vnLab.Data.Entities;

public class User : IdentityUser
{
    [MaxLength(50)]
    [Required]
    public string? FullName { get; set; }

    [Required]
    public bool? Sex { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [MaxLength(1000)]
    [Required]
    public string? Address { get; set; }

    [MaxLength(15)]
    [Required]
    public string? CCCD { get; set; }

    [MaxLength(50)]
    [Required]
    public string? TDHV { get; set; }

    public double BHXH { get; set; }

    public double BHYT { get; set; }

    public int SoPhep { get; set; }

    public bool? Status { get; set; }

    public int? PositionId { get; set; }

    public Position? Position { get; set; }

    public ICollection<TimeSheet>? TimeSheets { get; set; }
    public ICollection<Contract>? Contracts { get; set; }

}