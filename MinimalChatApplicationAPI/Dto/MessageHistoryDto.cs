namespace MinimalChatApplicationAPI.Dto
{
    public class MessageHistoryDto
    {
        public DateTime Before { get; set; } = DateTime.UtcNow;

        public int Count { get; set; } = 20;

        public string Sort { get; set; } = "asc";

    }
}
