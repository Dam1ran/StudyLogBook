using System;

namespace NutshellRepo.ViewModels.User
{
    public class ReadMessageViewModel
    {
        public string MessageId { get; set; }
#nullable enable
        public string? RepliedTo { get; set; }
#nullable disable
        public DateTime DateTimeSent { get; set; }
        public string FromUser { get; set; }
        public string Subject { get; set; }        
        public string Body { get; set; }

    }
}
