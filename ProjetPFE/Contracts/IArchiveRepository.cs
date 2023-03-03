using ProjetPFE.Dto;
using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface IArchiveRepository
    {
        
            public Task<IEnumerable<archive>> GetArchives();
            public Task<archive> GetArchive(int arch_id);
            public Task<archive> CreateArchive(ArchiveForCreationDto archive);
            public Task UpdateArchive(int arch_id, ArchiveForUpdateDto archive);
            public Task DeleteArchive(int arch_id);
       
    }
}
