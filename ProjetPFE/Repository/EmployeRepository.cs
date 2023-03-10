using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Entities;
using Dapper;

namespace ProjetPFE.Repository
{
    public class EmployeRepository : IEmployeRepository
    {

        private readonly DapperContext _context;
        private readonly IEmployeRepository EmployeRepository;

        public EmployeRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<ICollection<employe>> Getemployes()
        {
            var query = "SELECT employe.employe_id, employe.nom, employe.prenom, employe.matricule,  employe.matricule_resp, employe.fonction, employe.role, employe.date_recrutement, employe.email, employe.compte_winds, diplome.diplome_id as dipID , diplome.nom_diplome, diplome.lieu_diplome  FROM [dbo].[employe] as employe LEFT JOIN [dbo].[diplome] as diplome ON employe.employe_id = diplome.employe_id ";
            var EmployeDictionary = new Dictionary<int, employe>;
            using (var _context = this._context.CreateConnection())
            {
                IEnumerable<employe> result = await _context.QueryAsync<employe, diplome, employe>(query, (emp, diplome) =>
               {
                   employe employe;
                   if (!EmployeDictionary.TryGetValue(emp.employe_id, out employe))
                   {
                       employe = emp;
                       employe.diplomes = new List<diplome>();
                       EmployeDictionary.Add(employe.employe_id, employe);
                   }
                   if (diplome.employe_id > 0) employe.diplomes.Add(diplome);
                   return employe;
               }, splitOn: "dipID");
                var employes = result.Distinct().ToList();
                return employes;
            }
        }
        }
}
