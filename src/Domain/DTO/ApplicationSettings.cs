namespace Domain.DTO
{
    /// <summary>
    /// Represents application-level configuration settings.
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the authentication configuration.
        /// </summary>
        public AuthenticationSettings Authentication { get; set; } = new AuthenticationSettings();
        public string StoragePath { get; set; } = "";
        public string ProcessedStoragePath { get; set; } = "";
        public Gemini Gemini { get; set; } = new Gemini();
    }

    public class Gemini
    {
        public string ApiKey { get; set; } = "";
    }

    /// <summary>
    /// Represents authentication-related settings.
    /// </summary>
    public class AuthenticationSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether authentication is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
