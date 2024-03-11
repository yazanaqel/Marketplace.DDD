using Domain.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer.Configuration;
internal sealed class UserConfiguration {

    internal static void ConfigureApplicationUser(ModelBuilder modelBuilder) {

        modelBuilder.Entity<ApplicationUser>()
            .HasKey(k => k.Id)
            .IsClustered(false);
    }
}