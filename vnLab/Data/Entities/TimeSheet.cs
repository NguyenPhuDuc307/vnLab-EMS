using System.ComponentModel.DataAnnotations;

namespace vnLab.Data.Entities;

public class TimeSheet
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime? LastDayOfMoth { get; set; }
    public int TongNgayCong { get; set; }
    public int NghiCoPhep { get; set; }
    public int NghiKhongPhep { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public ICollection<TimeSheetDetail>? TimeSheetDetails { get; set; }
}