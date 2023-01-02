namespace bbxp.web.api.Configuration
{
    public class ApiConfiguration
    {
        public required string JWTSecret { get; set; }

        public required string JWTAudience { get; set; }

        public required string JWTIssuer { get; set; }

        public required string JWTSubject { get; set; }

        public required string JWTHashToken { get; set; }
    }
}