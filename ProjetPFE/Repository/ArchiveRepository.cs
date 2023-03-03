using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Repository
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly DapperContext _context;

        public ArchiveRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<archive>> GetArchives()
        {
            var query = "SELECT * FROM archive";

            using (var connection = _context.CreateConnection())
            {
                var archives = await connection.QueryAsync<archive>(query);
                return archives.ToList();
            }
        }

        public async Task<archive> GetArchive(int arch_id)
        {
            var query = "SELECT * FROM archive WHERE arch_id = @arch_id";

            using (var connection = _context.CreateConnection())
            {
                var archive = await connection.QuerySingleOrDefaultAsync<archive>(query, new { arch_id });

                return archive;
            }
        }


        public async Task<archive> CreateArchive(ArchiveForCreationDto archive)
        {
            var query = "INSERT INTO archive (compte_winds, demande_id) VALUES (@compte_winds, @demande_id)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("num_arch", archive.num_arch, DbType.Int32);
            parameters.Add("demande_id", archive.demande_id, DbType.Int32);



            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdarchive = new archive
                {
                    arch_id = id,
                    num_arch = archive.num_arch,
                    demande_id = archive.demande_id,
                };
                return createdarchive;
            }
        }

        public async Task UpdateArchive(int arch_id, ArchiveForUpdateDto archive)
        {
            var query = "UPDATE archive SET demande_id = @demande_id, num_arch = @num_arch WHERE arch_id = @arch_id";

            var parameters = new DynamicParameters();
            parameters.Add("arch_id", arch_id, DbType.Int32);
            parameters.Add("num_arch", archive.num_arch, DbType.Int32);
            parameters.Add("demande_id", archive.demande_id, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteArchive(int arch_id)
        {
            var query = "DELETE FROM archive WHERE arch_id = @arch_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { arch_id });
            }
        }
    }
}
