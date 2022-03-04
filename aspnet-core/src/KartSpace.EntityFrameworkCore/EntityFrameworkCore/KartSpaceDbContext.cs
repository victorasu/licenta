using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using KartSpace.Authorization.Roles;
using KartSpace.Authorization.Users;
using KartSpace.MultiTenancy;

namespace KartSpace.EntityFrameworkCore
{
    public class KartSpaceDbContext : AbpZeroDbContext<Tenant, Role, User, KartSpaceDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public KartSpaceDbContext(DbContextOptions<KartSpaceDbContext> options)
            : base(options)
        {
        }
    }
}
