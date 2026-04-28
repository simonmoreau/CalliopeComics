namespace Domain.DTO
{
    public class StatusDTO
    {
        public string Version { get; set; } = string.Empty;
        public bool DatabaseConnected { get; set; }
        public bool IsAuthenticated { get; set; }
        public ApplicationSettings ApplicationSettings { get; set; }
        public string PathAssembly { get; set; }
        public string DomainName { get; set; }
    }
}
