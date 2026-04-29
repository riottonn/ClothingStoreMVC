using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Domain; // Посилання на твій виправлений клас User

namespace ClothingStore.Infrastructure
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        // Цей метод можна залишити порожнім, якщо немає специфічних Fluent API налаштувань
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}