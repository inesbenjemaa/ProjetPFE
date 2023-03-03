using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DapperContext _context;
        public RoleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<role>> GetRoles()
        {
            var query = "SELECT * FROM role";

            using (var connection = _context.CreateConnection())
            {
                var Roles = await connection.QueryAsync<role>(query);
                return Roles.ToList();
            }
        }
        public async Task<role> GetRole(int role_id)
        {
            var query = "SELECT * FROM role WHERE role_id = @role_id";

            using (var connection = _context.CreateConnection())
            {
                var role = await connection.QuerySingleOrDefaultAsync<role>(query, new { role_id });

                return role;
            }
        }
        public async Task<role> CreateRole(RoleForCreationDto role)
        {
            var query = "INSERT INTO role (nom_role, demandeur_id) VALUES (@nom_role, @demandeur_id)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nom_role", role.nom_role, DbType.Int32);
            parameters.Add("demandeur_id", role.demandeur_id, DbType.Int32);
           
     


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdrole = new role
                {
                    role_id = id,
                    nom_role = role.nom_role,
                    demandeur_id = role.demandeur_id,
                   
                };
                return createdrole;
            }
        }

        public async Task UpdateRole(int role_id, RoleForUpdateDto role)
        {
            var query = "UPDATE role SET nom_role = @nom_role, demandeur_id    WHERE role_id = @role_id";

            var parameters = new DynamicParameters();
            parameters.Add("role_id", role_id, DbType.Int32);
            parameters.Add("nom_role", role.nom_role, DbType.Int32);
            parameters.Add("demandeur_id", role.demandeur_id, DbType.Int32);
           

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteRole(int role_id)
        {
            var query = "DELETE FROM role WHERE role_id = @role_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { role_id });
            }
        }

        

    }
}
