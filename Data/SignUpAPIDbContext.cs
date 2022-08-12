using Microsoft.EntityFrameworkCore;
using SignUpAPI.Models;

namespace SignUpAPI.Data
{
    public class SignUpAPIDbContext:DbContext
    {
        //public SignUpAPIDbContext(DbContextOptions options) : base(options)
        public SignUpAPIDbContext(DbContextOptions<SignUpAPIDbContext> options) : base(options)


        {
        }
        public DbSet<Signup> Sign { get; set; }
        public DbSet<Upload> Uploads { get; set; }
    }


}

