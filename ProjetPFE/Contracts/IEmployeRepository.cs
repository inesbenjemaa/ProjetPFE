using ProjetPFE.Entities;

namespace ProjetPFE.Contracts
{
    public interface IEmployeRepository
    {
        Task<ICollection<employe>> Getemployes();

    }
}
