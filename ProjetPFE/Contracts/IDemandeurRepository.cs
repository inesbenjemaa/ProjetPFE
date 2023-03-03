using ProjetPFE.Dto;
using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface IDemandeurRepository
    {
        public Task<IEnumerable<demandeur>> Getdemandeurs();
        public Task<demandeur> GetDemandeur(int demandeur_id);
        public Task<demandeur> CreateDemandeur(DemandeurForCreationDto demandeur);
        public Task UpdateDemandeur(int demandeur_id, DemandeurForUpdateDto demandeur);
        public Task DeleteDemandeur(int demandeur_id);
     
    }
     
}

