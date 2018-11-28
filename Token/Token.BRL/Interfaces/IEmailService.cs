namespace Token.BRL.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string subject, string message, bool isHtml);
    }
}