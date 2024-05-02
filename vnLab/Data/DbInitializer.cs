using Microsoft.AspNetCore.Identity;
using vnLab.Data.Entities;

namespace vnLab.Data
{
    public class DbInitializer
    {
        private readonly vnLabDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string AdminRoleName = "Admin";
        private readonly string UserRoleName = "Member";

        public DbInitializer(vnLabDbContext context,
          UserManager<User> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // Seeding role
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AdminRoleName,
                    NormalizedName = AdminRoleName.ToUpper(),
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = UserRoleName,
                    NormalizedName = UserRoleName.ToUpper(),
                });
            }

            // Seeding user
            if (!_userManager.Users.Any())
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "ducdm@gmail.com",
                    FullName = "Đặng Minh Đức",
                    Sex = true,
                    BirthDate = new DateTime(2002, 11, 1),
                    Address = "1 Pham Van Dong street, Cau Giay district, Ha Noi city",
                    CCCD = "123456789",
                    TDHV = "Dai hoc",
                    BHXH = 0.245,
                    BHYT = 0.045,
                    SoPhep = 0,
                    Status = true,
                    Email = "ducdm@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321"
                }, "Admin@123");
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("ducdm@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }

                var result1 = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "ducnp@gmail.com",
                    FullName = "Nguyễn Phú Đức",
                    Sex = true,
                    BirthDate = new DateTime(2002, 11, 1),
                    Address = "1 Pham Van Dong street, Cau Giay district, Ha Noi city",
                    CCCD = "123456789",
                    TDHV = "Dai hoc",
                    BHXH = 0.245,
                    BHYT = 0.045,
                    SoPhep = 0,
                    Status = true,
                    Email = "ducnp@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321"
                }, "Admin@123");
                if (result1.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("ducnp@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
                var result2 = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "phuongltn@gmail.com",
                    FullName = "Lê Thị Ngọc Phượng",
                    Sex = true,
                    BirthDate = new DateTime(2002, 11, 1),
                    Address = "1 Pham Van Dong street, Cau Giay district, Ha Noi city",
                    CCCD = "123456789",
                    TDHV = "Dai hoc",
                    BHXH = 0.245,
                    BHYT = 0.045,
                    SoPhep = 0,
                    Status = true,
                    Email = "phuongltn@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321"
                }, "Admin@123");
                if (result2.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("phuongltn@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
                var result3 = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "quyenntt@gmail.com",
                    FullName = "Nguyễn Thị Thu Quyên",
                    Sex = true,
                    BirthDate = new DateTime(2002, 11, 1),
                    Address = "1 Pham Van Dong street, Cau Giay district, Ha Noi city",
                    CCCD = "123456789",
                    TDHV = "Dai hoc",
                    BHXH = 0.245,
                    BHYT = 0.045,
                    SoPhep = 0,
                    Status = true,
                    Email = "quyenntt@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321"
                }, "Admin@123");
                if (result3.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("quyenntt@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
                var result4 = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "annd@gmail.com",
                    FullName = "Nguyễn Đức An",
                    Sex = true,
                    BirthDate = new DateTime(2002, 11, 1),
                    Address = "1 Pham Van Dong street, Cau Giay district, Ha Noi city",
                    CCCD = "123456789",
                    TDHV = "Dai hoc",
                    BHXH = 0.245,
                    BHYT = 0.045,
                    SoPhep = 0,
                    Status = true,
                    Email = "annd@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321"
                }, "Admin@123");
                if (result4.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("annd@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
                var result5 = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "linhnn@gmail.com",
                    FullName = "Nguyễn Ngọc Linh",
                    Sex = true,
                    BirthDate = new DateTime(2002, 11, 1),
                    Address = "1 Pham Van Dong street, Cau Giay district, Ha Noi city",
                    CCCD = "123456789",
                    TDHV = "Dai hoc",
                    BHXH = 0.245,
                    BHYT = 0.045,
                    SoPhep = 0,
                    Status = true,
                    Email = "linhnn@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321"
                }, "Admin@123");
                if (result5.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("linhnn@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
            }
        }
    }
}