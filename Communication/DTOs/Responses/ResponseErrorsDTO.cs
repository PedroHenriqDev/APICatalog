namespace Communication.DTOs.Responses
{
    public class ResponseErrorsDTO
    {
        public IList<string> ErrorMessages { get; set; } = new List<string>();

        public ResponseErrorsDTO(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
