namespace BookCatalog.Application.Common
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public List<string>? ErrorMessage { get; set; }

        private ServiceResponse(
            T? data,
            bool isSuccess,
            int statusCode,
            List<string>? errorMessage)
        {
            Data = data;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        // Success respones 
        public static ServiceResponse<T> Success(T data, int statusCode = 200)
        {
            return new ServiceResponse<T>(
                data: data,
                isSuccess: true,
                statusCode: statusCode,
                errorMessage: null
            );
        }

        // Error response - Single message
        public static ServiceResponse<T> Fail(string errorMessage, int statusCode = 400)
        {
            return new ServiceResponse<T>(
                data: default,
                isSuccess: false,
                statusCode: statusCode,
                errorMessage: new List<string> { errorMessage }
            );
        }

        // Error response - Multiple messages
        public static ServiceResponse<T> Fail(List<string> errorMessages, int statusCode = 400)
        {
            return new ServiceResponse<T>(
                data: default,
                isSuccess: false,
                statusCode: statusCode,
                errorMessage: errorMessages
            );
        }
    }
}
