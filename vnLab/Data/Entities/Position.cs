namespace vnLab.Data.Entities;

public class Position
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Status { get; set; }
    public int DivisionId { get; set; }
    public Division? Division { get; set; }
    public ICollection<User>? Users { get; set; }
}