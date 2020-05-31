
namespace Common.Response
{
    /// <summary>
    /// response class between internet and betblogger.WebApi
    /// all api methods must return this object
    /// </summary>
    public class ApiResponse<T> : Response
    {
        public string TypeText => Type.ToString("G");

        public T Data { get; set; }
    }

    public class ApiResponse : Response
    {
        public string TypeText => Type.ToString("G");
    }
}