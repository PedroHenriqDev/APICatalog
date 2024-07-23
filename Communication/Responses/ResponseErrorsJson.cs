namespace Communication.Responses
{
    public class ResponseErrorsJson
    {
        public IList<string> ErrorMessages { get; set; } = new List<string>();

        public ResponseErrorsJson(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
