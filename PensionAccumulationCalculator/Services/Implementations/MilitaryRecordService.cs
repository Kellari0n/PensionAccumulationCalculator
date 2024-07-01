using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;
using PensionAccumulationCalculator.Services.Interfaces;
using PensionAccumulationCalculator.Services.Responses;

namespace PensionAccumulationCalculator.Services.Implementations {
    internal class MilitaryRecordService : IMilitaryRecordService {
        private readonly IMilitaryRecordRepo _militaryRecordRepo;

        public MilitaryRecordService(IMilitaryRecordRepo militaryRecordRepo) {
            _militaryRecordRepo = militaryRecordRepo;
        }

        public async Task<BaseResponse<ICollection<Military_record>>> GetAllAsync() {
            return new BaseResponse<ICollection<Military_record>>() { Data = await _militaryRecordRepo.GetAllAsync(), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<Military_record>> GetByIdAsync(int id) {
            return new BaseResponse<Military_record>() { Data = await _militaryRecordRepo.GetByIdAsync(id), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(Military_record entity) {
            await _militaryRecordRepo.CreateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryDeleteAsync(int id) {
            await _militaryRecordRepo.DeleteAsync(id);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryUpdateAsync(Military_record entity) {
            await _militaryRecordRepo.UpdateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode= Enums.StatusCode.OK };
        }
    }
}
