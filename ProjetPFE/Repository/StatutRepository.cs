using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Repository
{
    public class StatutRepository : IStatutRepository
    {
        private readonly DapperContext _context;

        public StatutRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<statut_demande>> GetStatuts()
        {
            var query = "SELECT * FROM statut_demande";

            using (var connection = _context.CreateConnection())
            {
                var statuts = await connection.QueryAsync<statut_demande>(query);
                return statuts.ToList();
            }
        }

        public async Task<statut_demande> GetStatut(int statut_id)
        {
            var query = "SELECT * FROM statut_demande WHERE statut_id = @statut_id";

            using (var connection = _context.CreateConnection())
            {
                var statut = await connection.QuerySingleOrDefaultAsync<statut_demande>(query, new { statut_id });

                return statut;
            }
        }

        public async Task<statut_demande> CreateStatut(StatutForCreationDto statut_demande)
        {
            var query = "INSERT INTO statut_demande (statut, demande_id, validation_id ) VALUES (@statut, @demande_id, @validation_id)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            //parameters.Add("statut_id", statut_demande.statut_id, DbType.Int32);
            parameters.Add("demande_id", statut_demande.demande_id, DbType.Int32);
            parameters.Add("statut", statut_demande.statut, DbType.String);
            parameters.Add("validation_id", statut_demande.validation_id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdstatut = new statut_demande
                {
                    statut_id = id,
                    demande_id = statut_demande.demande_id,
                    statut = statut_demande.statut,
                    validation_id = statut_demande.validation_id,
                };
                return createdstatut;
            }
        }


        public async Task UpdateStatut(int statut_id, StatutForUpdateDto statut_demande)
        {
            var query = "UPDATE statut_demande SET demande_id = @demande_id, statut = @statut, validation_id = @validation_id WHERE statut_id = @statut_id";

            var parameters = new DynamicParameters();
            parameters.Add("statut_id", statut_id, DbType.Int32);
            parameters.Add("demande_id", statut_demande.demande_id, DbType.Int32);
            parameters.Add("statut", statut_demande.statut, DbType.String);
            parameters.Add("validation_id", statut_demande.validation_id, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


        public async Task DeleteStatut(int statut_id)
        {
            var query = "DELETE FROM statut_demande WHERE statut_id = @statut_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { statut_id });
            }
        }

       


    }
}

