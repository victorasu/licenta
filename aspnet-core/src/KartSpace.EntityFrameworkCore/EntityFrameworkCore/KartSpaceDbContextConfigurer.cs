using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace KartSpace.EntityFrameworkCore
{
    public static class KartSpaceDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<KartSpaceDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<KartSpaceDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
