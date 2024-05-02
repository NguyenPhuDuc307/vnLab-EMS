namespace vnLab.Data.Entities;

public class TimeSheetDetail
{
    public int Id { get; set; }
    public int TimeSheetId { get; set; }
    public TimeSheet? TimeSheet { get; set; }
    public DateTime TimeCheckIn { get; set; }
    public DateTime? TimeCheckOut { get; set; }
}