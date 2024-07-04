using System.Net;

namespace PensionAccumulationCalculator.Services.Responses {
    public class BaseResponse<DataT>
    {
        public DataT? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}