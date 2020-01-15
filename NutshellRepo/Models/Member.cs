using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Models
{
    public class Member : IdentityUser
    {
        public DateTime RegisteredOn { get; set; }
        public int AppreciatiationPoints { get; set; }
        public int UnreadMessages { get; set; }
        public int Nutshells { get; set; }
        public string ProfileStatus { get; set; }
        public bool ShowWelcomeMessage { get; set; }
#nullable enable
        [MaxLength(256)]
        public string? PhotoPath { get; set; }
#nullable disable
    }
}
