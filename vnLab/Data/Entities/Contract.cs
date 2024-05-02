using System.ComponentModel.DataAnnotations;

namespace vnLab.Data.Entities;

public class Contract
{
    public int Id { get; set; }
    public string? Number { get; set; }
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public double HeSoLuong { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}