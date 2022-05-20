namespace ModularMonolithSpike.Middlewares
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return $"{{\"{nameof(StatusCode)}\":{StatusCode},\"{nameof(Message)}\":\"{Message}\"}}";
        }
    }
}
