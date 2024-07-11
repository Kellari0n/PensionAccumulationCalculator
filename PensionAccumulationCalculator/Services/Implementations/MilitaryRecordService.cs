using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;
using PensionAccumulationCalculator.Services.Interfaces;
using PensionAccumulationCalculator.Services.Responses;

using System.Net;
using System.Xml;

namespace PensionAccumulationCalculator.Services.Implementations {
    internal class MilitaryRecordService : IMilitaryRecordService {
        private readonly IMilitaryRecordRepo _recordRepo;

        public MilitaryRecordService(IMilitaryRecordRepo militaryRecordRepo) {
            _recordRepo = militaryRecordRepo;
        }

        public async Task<BaseResponse<XmlDocument>> ExportXmlAsync() {
            return new BaseResponse<XmlDocument> { Data = await _recordRepo.ExportXmlAsync(), StatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse<XmlDocument>> ExportXmlByIdAsync(int id) {
            return new BaseResponse<XmlDocument> { Data = await _recordRepo.ExportXmlByIdAsync(id), StatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse<ICollection<Military_record>>> GetAllAsync() {
            ICollection<Military_record> data;
            HttpStatusCode statusCode;
            string description;

            try {
                data = await _recordRepo.GetAllAsync();
                if (data.Count == 0) {
                    statusCode = HttpStatusCode.NoContent;
                    description = "Zero entities found";
                }
                else {
                    statusCode = HttpStatusCode.OK;
                    description = "Successfully";
                }
            }
            catch (TimeoutException) {
                data = [];
                statusCode = HttpStatusCode.GatewayTimeout;
                description = "Время ожидания истекло.";
            }
            catch (Exception) {
                data = [];
                statusCode = HttpStatusCode.InternalServerError;
                description = "Что-то пошло не так.";
            }

            return new BaseResponse<ICollection<Military_record>> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<Military_record>> GetByIdAsync(int id) {
            Military_record data;
            HttpStatusCode statusCode;
            string description;

            try {
                data = await _recordRepo.GetByIdAsync(id);
                if (data is null) {
                    statusCode = HttpStatusCode.NoContent;
                    description = "Zero entities found";
                }
                else {
                    statusCode = HttpStatusCode.OK;
                    description = "Successfully";
                }
            }
            catch (TimeoutException) {
                data = null!;
                statusCode = HttpStatusCode.GatewayTimeout;
                description = "Время ожидания истекло.";
            }
            catch (Exception) {
                data = null!;
                statusCode = HttpStatusCode.InternalServerError;
                description = "Что-то пошло не так.";
            }

            return new BaseResponse<Military_record> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(Military_record entity) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _recordRepo.TryCreateAsync(entity);

                if (res) {
                    data = true;
                    statusCode = HttpStatusCode.OK;
                    description = "Entity successfully created";
                }
                else {
                    data = false;
                    statusCode = HttpStatusCode.InternalServerError;
                    description = "Entity wasn't created";
                }
            }
            catch (TimeoutException) {
                data = false;
                statusCode = HttpStatusCode.GatewayTimeout;
                description = "Время ожидания истекло.";
            }
            catch (Exception) {
                data = false;
                statusCode = HttpStatusCode.InternalServerError;
                description = "Что-то пошло не так.";
            }

            return new BaseResponse<bool> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<bool>> TryDeleteAsync(int id) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _recordRepo.TryDeleteAsync(id);

                if (res) {
                    data = true;
                    statusCode = HttpStatusCode.OK;
                    description = "Entity successfully deleted";
                }
                else {
                    data = false;
                    statusCode = HttpStatusCode.InternalServerError;
                    description = "Entity wasn't deleted";
                }
            }
            catch (TimeoutException) {
                data = false;
                statusCode = HttpStatusCode.GatewayTimeout;
                description = "Время ожидания истекло.";
            }
            catch (Exception) {
                data = false;
                statusCode = HttpStatusCode.InternalServerError;
                description = "Что-то пошло не так.";
            }

            return new BaseResponse<bool> { Data = data, StatusCode = statusCode, Description = description };
        }

        public Task<BaseResponse<bool>> TryImportXmlAsync(XmlDocument xml) {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> TryUpdateAsync(Military_record entity) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _recordRepo.TryUpdateAsync(entity);

                if (res) {
                    data = true;
                    statusCode = HttpStatusCode.OK;
                    description = "Entity successfully updated";
                }
                else {
                    data = false;
                    statusCode = HttpStatusCode.InternalServerError;
                    description = "Entity wasn't updated";
                }
            }
            catch (TimeoutException) {
                data = false;
                statusCode = HttpStatusCode.GatewayTimeout;
                description = "Время ожидания истекло.";
            }
            catch (Exception) {
                data = false;
                statusCode = HttpStatusCode.InternalServerError;
                description = "Что-то пошло не так.";
            }

            return new BaseResponse<bool> { Data = data, StatusCode = statusCode, Description = description };
        }
    }
}
