using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PP3.DatabaseContext
{
    public class MessageContext : DbContext
    {
        public DbSet<MessageModel> Messages { get; set; }
        public MessageContext() : base()
        {
            Database.EnsureCreated();
        }

        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=PP;Trusted_Connection=True");
        }

    }
}
