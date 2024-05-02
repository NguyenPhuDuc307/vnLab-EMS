using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vnLab.Data.Entities;

namespace vnLab.Data;

public class vnLabDbContext : IdentityDbContext<User>
{
    public vnLabDbContext(DbContextOptions<vnLabDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        modelBuilder.Entity<User>().ToTable("Users").Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
    }

    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<Division> Divisions { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<SalarySheet> SalarySheets { get; set; } = null!;
    public DbSet<TimeSheet> TimeSheets { get; set; } = null!;
    public DbSet<TimeSheetDetail> TimeSheetDetails { get; set; } = null!;
}