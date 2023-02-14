namespace ECommerce.Service.Helpers
{
    public class Response<TResult>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TResult Result { get; set; }
    }
}
