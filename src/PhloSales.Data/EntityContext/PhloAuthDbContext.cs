using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhloSales.Data.Entities;

namespace PhloSales.Data.EntityContext;

public class PhloAuthDbContext : IdentityUserContext<ApplicationUser, int>
{
    public PhloAuthDbContext(DbContextOptions<PhloAuthDbContext> options)
             : base(options)
    {
    }

}



