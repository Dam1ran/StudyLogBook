namespace NutshellRepo.ViewModels.User
{
    public class UserPreviewMessage
    {
        public string Id { get; set; }
        public string FromUser { get; set; }
#nullable enable
        public string? Subject { get; set; }
#nullable disable
        public string MessageBody { get; set; }
        public bool isToDelete { get; set; } = false;
        public bool isRead { get; set; } = false;

    }
}
