using ProjetPFE.Dto;
using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface IRoleRepository
    {
        
        public Task<IEnumerable<role>> GetRoles();
        public Task<role> GetRole(int role_id);
        public Task<role> CreateRole(RoleForCreationDto role);
        public Task UpdateRole(int role_id, RoleForUpdateDto role);
        public Task DeleteRole(int role_id);
        
    
}
}
