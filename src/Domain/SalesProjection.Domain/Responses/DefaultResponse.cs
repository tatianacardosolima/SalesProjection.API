namespace SalesProjection.Domain.Responses
{
    public class DefaultResponse<T> where T : class
    {
        public DefaultResponse(T data, string message, bool isSuccess) { 
            Data = data;
            Message = message;  
            IsSuccess = isSuccess;
        }

        public DefaultResponse(string message, bool isSuccess)
        {
            Data = null;
            Message = message;
            IsSuccess = isSuccess;
        }
        public String Message { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}
