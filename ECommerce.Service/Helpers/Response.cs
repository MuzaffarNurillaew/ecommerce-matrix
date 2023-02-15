namespace ECommerce.Service.Helpers
{
    public class Response<TResult>
    {
        public int StatusCode { get; set; } = 404;
        public string Message { get; set; } = "Not Found";
        public TResult Result { get; set; }
    }
}
