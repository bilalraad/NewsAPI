namespace NewsAPI.Errors
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }


        public AppException(string message, int StatusCode = 400) : base(message)
        {
            this.StatusCode = StatusCode;
        }

        // not found 
        public static AppException NotFound(string message)
        {
            return new AppException(message, 404);
        }

        public static AppException BadRequest(string message)
        {
            return new AppException(message);
        }

        // unauthorized
        public static AppException Unauthorized(string message)
        {
            return new AppException(message, 401);
        }

    }
}