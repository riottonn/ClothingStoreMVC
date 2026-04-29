using Microsoft.AspNetCore.Identity;

namespace ClothingStore.Domain
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}