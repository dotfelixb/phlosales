using Microsoft.AspNetCore.Identity;

namespace PhloSales.Data.Entities;

public class ApplicationUser : IdentityUser<int>
{
}

public class ApplicationRole : IdentityRole<int>
{
}