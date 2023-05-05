using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

namespace TodoWebApi.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todo { set; get; }
        public DbSet<User> User { set; get; }
        public DbSet<UserHasTodos> UserHasTodos { set; get; }
        public DbSet<Role> Role { set; get; }
        public DbSet<RoleHasPermissions> RoleHasPermissions { set; get; }
        public DbSet<Permission> Permission { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserHasTodos>().HasOne<User>(uhs => uhs.User).WithMany(u => u.UserHasTodos)
                .HasForeignKey(uhs => uhs.uid);


            modelBuilder.Entity<UserHasTodos>().HasOne<Todo>(uhs => uhs.Todo).WithMany(t => t.UserHasTodos)
                .HasForeignKey(uhs => uhs.todoId);
            
            modelBuilder.Entity<UserHasTodos>().HasKey(uht => new { uht.todoId, uht.uid });

            modelBuilder.Entity<Role>().HasOne<User>(r => r.User).WithMany(u => u.Role).HasForeignKey(r => r.uid);

            modelBuilder.Entity<RoleHasPermissions>().HasOne<Role>(rhp => rhp.Role).WithMany(r => r.RoleHasPermissions)
                .HasForeignKey(rhp => rhp.rid);

            modelBuilder.Entity<RoleHasPermissions>().HasOne<Permission>(rhp => rhp.Permission)
                .WithMany(p => p.RoleHasPermissions).HasForeignKey(rhp => rhp.permissionId);

            modelBuilder.Entity<RoleHasPermissions>().HasKey(rhp => new {  rhp.permissionId, rhp.rid });
        }
    }

}