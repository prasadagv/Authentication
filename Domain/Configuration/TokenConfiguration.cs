namespace Authentication.Domain.Configuration
{
    public class TokenConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }
    }
}
