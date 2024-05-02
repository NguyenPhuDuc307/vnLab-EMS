namespace vnLab.Data.Entities;

public class Division
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Status { get; set; }
    public ICollection<Position>? Positions { get; set; }
}