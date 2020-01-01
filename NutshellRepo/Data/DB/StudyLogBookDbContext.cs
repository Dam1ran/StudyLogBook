using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    }
}
