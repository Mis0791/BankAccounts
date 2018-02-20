using System;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Models
{
    public class BankContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<RegisterUser> User { get; set; }
        public DbSet<Account> Account { get; set; }

    }
}