using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.ViewModels.User
{
    public class UserPreviewMessageViewModel
    {
        public string Id { get; set; }
        public string FromUser { get; set; }
#nullable enable
        public string? Subject { get; set; }
#nullable disable
        public string MessageBody { get; set; }
        public bool isToDelete { get; set; } = false;

    }
}
