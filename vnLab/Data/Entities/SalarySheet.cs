namespace vnLab.Data.Entities;

public class SalarySheet
{
    public int Id { get; set; }
    public int TimeSheetId { get; set; }
    public TimeSheet? TimeSheet { get; set; }
    public double LuongChinh { get; set; }
    public double PhuCap { get; set; }
    public double TienThuong { get; set; }
    public double NetSalary { get; set; }
    public DateTime DateCreated { get; set; }
}