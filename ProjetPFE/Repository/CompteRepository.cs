using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Repository
{
    public class CompteRepository : ICompteRepository
    {
        private readonly DapperContext _context;

        public CompteRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<compte>> GetComptes()
        {
            var query = "SELECT * FROM compte";

            using (var connection = _context.CreateConnection())
            {
                var comptes = await connection.QueryAsync<compte>(query);
                return comptes.ToList();
            }
        }

        public async Task<compte> GetCompte(int compte_id)
        {
            var query = "SELECT * FROM compte WHERE compte_id = @compte_id";

            using (var connection = _context.CreateConnection())
            {
                var compte = await connection.QuerySingleOrDefaultAsync<compte>(query, new { compte_id });

                return compte;
            }
        }


        public async Task<compte> CreateCompte(CompteForCreationDto compte)
        {
            var query = "INSERT INTO compte (compte_winds, demandeur_id) VALUES (@compte_winds, @demandeur_id)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("demandeur_id", compte.demandeur_id, DbType.Int32);
            parameters.Add("compte_winds", compte.compte_winds, DbType.String);
            


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdcompte = new compte
                {
                    compte_id = id,
                    demandeur_id = compte.demandeur_id,
                    compte_winds = compte.compte_winds,
                };
                return createdcompte;
            }
        }

        public async Task UpdateCompte(int compte_id, CompteForUpdateDto compte)
        {
            var query = "UPDATE compte SET demandeur_id = @demandeur_id, compte_winds = @compte_winds WHERE compte_id = @compte_id";

            var parameters = new DynamicParameters();
            parameters.Add("compte_id", compte_id, DbType.Int32);
            parameters.Add("demandeur_id", compte.demandeur_id, DbType.Int32);
            parameters.Add("compte_winds", compte.compte_winds, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCompte(int compte_id)
        {
            var query = "DELETE FROM compte WHERE compte_id = @compte_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { compte_id });
            }
        }

    }
}
