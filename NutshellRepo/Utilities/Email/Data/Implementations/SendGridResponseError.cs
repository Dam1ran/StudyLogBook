namespace NutshellRepo.Utilities.Email.Data.Implementations
{

    /// <summary>
    /// Containing fields with a SendGrid Response Errors.
    /// </summary>
    public class SendGridResponseError
    {
        public string Message { get; set; }
        public string Field { get; set; }
        public string Help { get; set; }
    }
}