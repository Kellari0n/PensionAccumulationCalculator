using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;
using PensionAccumulationCalculator.Services.Interfaces;
using PensionAccumulationCalculator.Services.Responses;

namespace PensionAccumulationCalculator.Services.Implementations {
    internal class InsuranceRecordService : IInsuranceRecordService {
        private readonly IInsuranceRecordRepo _insuranceRecordRepo;

        public InsuranceRecordService(IInsuranceRecordRepo insuranceRecordRepo) {
            _insuranceRecordRepo = insuranceRecordRepo;
        }

        public async Task<BaseResponse<ICollection<Insurance_record>>> GetAllAsync() {
            return new BaseResponse<ICollection<Insurance_record>>() { Data = await _insuranceRecordRepo.GetAllAsync(), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<Insurance_record>> GetByIdAsync(int id) {
            return new BaseResponse<Insurance_record> { Data = await _insuranceRecordRepo.GetByIdAsync(id), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(Insurance_record entity) {
            await _insuranceRecordRepo.CreateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryDeleteAsync(int id) {
            await _insuranceRecordRepo.DeleteAsync(id);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryUpdateAsync(Insurance_record entity) {
            await _insuranceRecordRepo.UpdateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }
    }
}
