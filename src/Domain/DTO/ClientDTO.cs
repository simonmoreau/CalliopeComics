using Domain.Entities;

namespace Domain.DTO
{
    public class ClientDTO
    {
        public ClientDTO() { }
        public ClientDTO(CLIENT CalliopeComicsClient)
        {
            ClientId = CalliopeComicsClient.CLIENTID;
            Name = CalliopeComicsClient.NAME;
            Ip = CalliopeComicsClient.IP;
            FtpIp = CalliopeComicsClient.FTP_IP;
            FtpPath = CalliopeComicsClient.FTP_PATH;
            Jingle = CalliopeComicsClient.JINGLE;
        }

        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string FtpIp { get; set; }
        public string FtpPath { get; set; }
        public bool Jingle { get; set; }
    }
}