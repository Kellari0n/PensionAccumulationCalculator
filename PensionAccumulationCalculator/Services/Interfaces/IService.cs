using PensionAccumulationCalculator.Services.Responses;

using System.Xml;

namespace PensionAccumulationCalculator.Services.Interfaces {
    public interface IService<DataT> {
        public Task<BaseResponse<bool>> TryCreateAsync(DataT entity);
        public Task<BaseResponse<bool>> TryUpdateAsync(DataT entity);
        public Task<BaseResponse<bool>> TryDeleteAsync(int id);
        public Task<BaseResponse<DataT>> GetByIdAsync(int id);
        public Task<BaseResponse<ICollection<DataT>>> GetAllAsync();
        public Task<BaseResponse<XmlDocument>> ExportXmlByIdAsync(int id);
        public Task<BaseResponse<XmlDocument>> ExportXmlAsync();
        public Task<BaseResponse<bool>> TryImportXmlAsync(XmlDocument xml);
    }
}
