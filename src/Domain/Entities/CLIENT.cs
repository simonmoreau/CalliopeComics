using Domain.Common;

namespace Domain.Entities
{
    public class CLIENT : AuditableEntity
    {
        public CLIENT() : base() { }

        public int CLIENTID { get; set; }
        public string NAME { get; set; }
        public string IP { get; set; }
        public string FTP_IP { get; set; }
        public string FTP_PATH { get; set; }
        public bool JINGLE { get; set; }
        public int LAST_AUDIO_INDEX { get; set; } = 0;
    }
}