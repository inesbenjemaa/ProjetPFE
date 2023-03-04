using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Repository
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly DapperContext _context;

        public ValidationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<valid_dem>> GetValidations()
        {
            var query = "SELECT * FROM valid_dem";

            using (var connection = _context.CreateConnection())
            {
                var validations = await connection.QueryAsync<valid_dem>(query);
                return validations.ToList();
            }
        }
        public async Task<valid_dem> GetValidation(int validation_id)
        {
            var query = "SELECT * FROM valid_dem WHERE validation_id = @validation_id";

            using (var connection = _context.CreateConnection())
            {
                var validation = await connection.QuerySingleOrDefaultAsync<valid_dem>(query, new { validation_id });

                return validation;
            }
        }
        public async Task<valid_dem> CreateValidation(ValidationForCreationDto valid_dem)
        {
            var query = "INSERT INTO valid_dem (validation, demandeur_id ) VALUES (@validation, @demandeur_id)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            //parameters.Add("validation_id", valid_dem.validation_id, DbType.Int32);
            parameters.Add("demandeur_id", valid_dem.demandeur_id, DbType.Int32);
            parameters.Add("validation", valid_dem.validation, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdvalidation = new valid_dem
                {
                    validation_id = id,
                    demandeur_id = valid_dem.demandeur_id,
                    validation = valid_dem.validation,
                };
                return createdvalidation;
            }
        }

        public async Task UpdateValidation(int validation_id, ValidationForUpdateDto valid_dem)
        {
            var query = "UPDATE valid_dem SET demandeur_id = @demandeur_id, validation = @validation WHERE validation_id = @validation_id";

            var parameters = new DynamicParameters();
            parameters.Add("validation_id", validation_id, DbType.Int32);
            parameters.Add("demandeur_id", valid_dem.demandeur_id, DbType.Int32);
            parameters.Add("validation", valid_dem.validation, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteValidation(int validation_id)
        {
            var query = "DELETE FROM valid_dem WHERE validation_id = @validation_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { validation_id });
            }
        }
    }
}
