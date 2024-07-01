using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;
using PensionAccumulationCalculator.Services.Interfaces;
using PensionAccumulationCalculator.Services.Responses;

namespace PensionAccumulationCalculator.Services.Implementations {
    internal class WorkRecordService : IWorkRecordService {
        private readonly IWorkRecordRepo _workRecordRepo;

        public WorkRecordService(IWorkRecordRepo workRecordRepo) {
            _workRecordRepo = workRecordRepo;
        }

        public async Task<BaseResponse<ICollection<Work_record>>> GetAllAsync() {
            return new BaseResponse<ICollection<Work_record>> { Data = await _workRecordRepo.GetAllAsync(), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<Work_record>> GetByIdAsync(int id) {
            return new BaseResponse<Work_record> { Data = await _workRecordRepo.GetByIdAsync(id), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(Work_record entity) {
            await _workRecordRepo.CreateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryDeleteAsync(int id) {
            await _workRecordRepo.DeleteAsync(id);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryUpdateAsync(Work_record entity) {
            await _workRecordRepo.UpdateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode= Enums.StatusCode.OK };
        }
    }
}
