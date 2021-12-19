using ListingApi.Model.Auth;
using ListingApi.Model.List;
using Microsoft.EntityFrameworkCore;

namespace ListingApi.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<ListItem> listItem { get; set; }
        public DbSet<MasterList> masterList { get; set; }
        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);
        }
    }
}