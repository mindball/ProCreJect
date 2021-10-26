namespace DbFactory.Models
{
    public class Message
    {
        public string BasicHeader { get; set; }

        public string ApplicationHeader { get; set; }

        public int TextBlockId { get; set; }

        public string Trailer { get; set; }
    }
}
