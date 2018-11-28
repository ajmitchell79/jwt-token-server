namespace Token.BRL.Common
{
    public enum TokenType
    {
        // AllClaims =1,
        // NameClaim =2,
        // NameLocationClaim=3
        Bearer

    }
    public class AppSettings
    {
        public string Secret { get; set; }

        public int TokenExpiryHours { get; set; }
    }

    public class Email
    {
        public string SmtpHost { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }

        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }

    }
}