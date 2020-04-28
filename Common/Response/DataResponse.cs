namespace Common.Response
{
    /// <summary>
    /// response class for data returned method
    /// </summary>
    public class DataResponse<T> : ResponseBase
    {
        public T Data { get; set; }
    }
}