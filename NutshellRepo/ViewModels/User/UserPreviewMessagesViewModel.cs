using System.Collections.Generic;

namespace NutshellRepo.ViewModels.User
{
    public class UserPreviewMessagesViewModel
    {
        public List<UserPreviewMessage> UserPreviewMessages { get; set; }         
        public int NumberOfMessages { get; set; }
        public int PageNumber { get; set; }
        public bool isFirstPage { get; set; }
        public bool isLastPage { get; set; }        
    }
}
