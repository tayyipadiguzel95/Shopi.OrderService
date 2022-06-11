namespace OrderService.Infrastructure.Public.Result

{
    /// <summary>
    /// ClientServiceResponse
    /// </summary>
    public class ClientServiceResponse
    {
        public ClientServiceResponse(ClientResultCode typeCode, string title, string message)
        {
            TypeCode = typeCode;
            Title = title;
            Messages = new List<string> {message};
        }

        public ClientResultCode TypeCode { get; set; }
        public string Title { get; set; }
        public List<string> Messages { get; set; }
    }
    
    /// <summary>
    /// ResultCode
    /// </summary>
    public enum ClientResultCode
    {
        Error = 500,
        Validation = 400,
        Warning = 420,
        NoContent = 204,
    }
}