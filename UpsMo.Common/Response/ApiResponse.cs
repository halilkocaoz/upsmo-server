namespace UpsMo.Common.Response
{
    public class ApiResponse
    {
        public ApiResponse(ResponseStatus responseStatus)
        {
            Data = default;
            StatusCode = (int)responseStatus;
            Succes = StatusCode < 400;

            Message = Succes ? "successful" : "failed";
        }

        public ApiResponse(ResponseStatus responseStatus, string message)
        {
            Data = default;
            StatusCode = (int)responseStatus;
            Succes = StatusCode < 400;

            Message = message;
        }

        public ApiResponse(ResponseStatus responseStatus, object data)
        {
            Data = data;
            StatusCode = (int)responseStatus;
            Succes = StatusCode < 400;

            Message = Succes ? "successful" : "failed";
        }

        public ApiResponse(ResponseStatus responseStatus, object data, string message)
        {
            Data = data;
            StatusCode = (int)responseStatus;
            Succes = StatusCode < 400;
            
            Message = message;
        }

        public object Data { get; private set; }

        public bool Succes { get; private set; }

        public string Message { get; private set; }

        public int StatusCode { get; private set; }
    }
}