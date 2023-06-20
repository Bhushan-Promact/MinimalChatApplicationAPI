namespace MinimalChatApplicationAPI.CustomExceptions
{
    public class BadDataException : Exception
    {
        public BadDataException(string message) : base(message)
        {

        }
    }
}
