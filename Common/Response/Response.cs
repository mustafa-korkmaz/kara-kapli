
namespace Common.Response
{
    /// <summary>
    /// response  base class
    /// </summary>
    public class Response
    {
        public string ErrorCode { get; set; }
        public ResponseType Type { get; set; }
    }
}