using PensionAccumulationCalculator.Enums;

namespace PensionAccumulationCalculator.Services.Responses {
    public class BaseResponse<DataT>
    {
        public required DataT Data { get; set; }
        public required StatusCode StatusCode { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}