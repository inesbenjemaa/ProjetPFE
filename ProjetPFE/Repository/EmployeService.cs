using AutoMapper;
using ProjetPFE.Contracts;
using ProjetPFE.Contracts.services;
using ProjetPFE.Dto;

namespace ProjetPFE.Repository
{
    public class EmployeService : IEmployeService
    {
        private readonly IEmployeRepository employeRepository;
        private readonly IMapper map;

        public EmployeService(IEmployeRepository employeRepository, IMapper map)
        {
            this.employeRepository = employeRepository;
            this.map = map;
        }
        public async Task<ICollection<EmployeDto>> RetrieveEmployes()
        {
            var employes = await this.employeRepository.Getemployes();

            var EmployeDto = this.map.Map<ICollection<EmployeDto>>(employes);
            return EmployeDto;

        }

    }
}
