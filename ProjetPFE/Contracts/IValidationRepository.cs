using ProjetPFE.Dto;
using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface IValidationRepository
    {
        public Task<IEnumerable<valid_dem>> GetValidations();
        public Task<valid_dem> GetValidation(int validation_id);
        public Task<valid_dem> CreateValidation(ValidationForCreationDto valid_dem);
        public Task UpdateValidation(int validation_id, ValidationForUpdateDto valid_dem);
        public Task DeleteValidation(int validation_id);
    }
}
