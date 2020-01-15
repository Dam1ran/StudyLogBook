using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutshellRepo.Models
{
    public class UserMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
#nullable enable
        [MaxLength(256)]
        public string? Subject { get; set; }
        [MaxLength(450)]
        public string? ToUserId { get; set; }
        [MaxLength(50)]
        public string? FromUser { get; set; }
#nullable disable
        [Required]
        [Display(Name = "Message")]
        [MaxLength(1000)]
        public string MessageBody { get; set; }
        public DateTime DateTimeSent { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
