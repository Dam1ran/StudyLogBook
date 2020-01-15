using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutshellRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Data.DB
{
    public class StudyLogBookDbContext : IdentityDbContext
    {
        public StudyLogBookDbContext(DbContextOptions<StudyLogBookDbContext> options) 
               : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<UserMessage> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMessage>()
                .HasIndex(b => b.ToUserId);
        }

    }
}
