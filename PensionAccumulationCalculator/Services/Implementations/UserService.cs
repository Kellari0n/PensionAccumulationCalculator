using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;
using PensionAccumulationCalculator.Services.Interfaces;
using PensionAccumulationCalculator.Services.Responses;

using System.Net;
using System.Xml;

namespace PensionAccumulationCalculator.Services.Implementations {
    internal class UserService : IUserService {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo) {
            _userRepo = userRepo;
        }

        public async Task<BaseResponse<XmlDocument>> ExportXmlAsync() {
            return new BaseResponse<XmlDocument> { Data = await _userRepo.ExportXmlAsync(), StatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse<XmlDocument>> ExportXmlByIdAsync(int id) {
            return new BaseResponse<XmlDocument> { Data = await _userRepo.ExportXmlByIdAsync(id), StatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse<ICollection<User>>> GetAllAsync() {
            ICollection<User> data;
            HttpStatusCode statusCode;
            string description;

            try {
                data = await _userRepo.GetAllAsync();
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

            return new BaseResponse<ICollection<User>> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<User>> GetByIdAsync(int id) {
            User data;
            HttpStatusCode statusCode;
            string description;

            try {
                data = await _userRepo.GetByIdAsync(id);
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

            return new BaseResponse<User> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<Client>> GetClientByIdAsync(int id) {
            Client data;
            HttpStatusCode statusCode;
            string description;

            try {
                data = await _userRepo.GetClientByIdAsync(id);
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

            return new BaseResponse<Client> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<ICollection<Client>>> GetClientsAsync() {
            ICollection<Client> data;
            HttpStatusCode statusCode;
            string description;

            try {
                data = await _userRepo.GetClientsAsync();
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

            return new BaseResponse<ICollection<Client>> { Data = data, StatusCode = statusCode, Description = description };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(User user, Client client) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _userRepo.TryCreateAsync(user, client);

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

        public async Task<BaseResponse<bool>> TryCreateAsync(User entity) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _userRepo.TryCreateAsync(entity);

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
                var res = await _userRepo.TryDeleteAsync(id);

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

        public async Task<BaseResponse<bool>> TryImportXmlAsync(XmlDocument xml) {
            return new BaseResponse<bool> { Data = await _userRepo.TryImportXmlAsync(xml), StatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryUpdateAsync(User user) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _userRepo.TryUpdateAsync(user);

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

        public async Task<BaseResponse<bool>> TryUpdateClientAsync(Client client) {
            bool data;
            HttpStatusCode statusCode;
            string description;

            try {
                var res = await _userRepo.TryUpdateClientAsync(client);

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
