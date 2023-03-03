using ProjetPFE.Dto;
using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface IStatutRepository
    {
        public Task<IEnumerable<statut_demande>> GetStatuts();
        public Task<statut_demande> GetStatut(int statut_id);
        public Task<statut_demande> CreateStatut(StatutForCreationDto statut_demande);
        public Task UpdateStatut(int statut_id, StatutForUpdateDto statut_demande);
        public Task DeleteStatut(int statut_id);
    }
}
