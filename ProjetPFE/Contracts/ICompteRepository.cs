using ProjetPFE.Dto;
using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface ICompteRepository
    {
        public Task<IEnumerable<compte>> GetComptes();
        public Task<compte> GetCompte(int compte_id);
        public Task<compte> CreateCompte(CompteForCreationDto compte);
        public Task UpdateCompte(int compte_id, CompteForUpdateDto compte);
        public Task DeleteCompte(int compte_id);
    }
}
