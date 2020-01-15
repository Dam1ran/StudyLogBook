public class Author
{
    public string UserId{get;set;}
    public string UserName{get;set;}
    public string Email{get;set;}
    public DateTime Registred{get;set;}//mby not set;
    public string? PhotoPath{get;set;}
    public int? AppreciationPoints{get;set;}
    public List<string> Messages{get;set;}
    public Message Message{get;set;}
}