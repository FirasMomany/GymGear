namespace API.Errors
{
    public class APIErrorResponse
    {
        public APIErrorResponse(int statusCode,string message,string? details)
        {
            StatusCode = statusCode;
            ExceptionMessage = message;
            Details = details;

        }

        public int StatusCode { get; set; }

        public string ExceptionMessage { get; set; }

        public string? Details { get; set; }





    }
}
