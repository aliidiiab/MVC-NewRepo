using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Models
{
    
    public class eCommerceAppEntities:IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public eCommerceAppEntities() : base()
        {
        }
        public eCommerceAppEntities(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=eCommerceDBv4;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
