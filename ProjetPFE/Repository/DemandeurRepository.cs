using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Entities;
using Dapper;
using System.Data;
using ProjetPFE.Dto;

namespace ProjetPFE.Repository
{
    public class DemandeurRepository : IDemandeurRepository
    {
        private readonly DapperContext _context;
        public DemandeurRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<demandeur>> Getdemandeurs()
        {
            var query = "SELECT * FROM demandeur";

            using (var connection = _context.CreateConnection())
            {
                var demandeurs = await connection.QueryAsync<demandeur>(query);
                return demandeurs.ToList();
            }
        }
        public async Task<demandeur> GetDemandeur(int demandeur_id)
        {
            var query = "SELECT * FROM demandeur WHERE demandeur_id = @demandeur_id";

            using (var connection = _context.CreateConnection())
            {
                var demandeur = await connection.QuerySingleOrDefaultAsync<demandeur>(query, new { demandeur_id });

                return demandeur;
            }
        }
        public async Task<demandeur> CreateDemandeur(DemandeurForCreationDto demandeur)
        {
            var query = "INSERT INTO demandeur (nom, prenom, matricule, fonction, matricule_resp,  email, compte_winds) VALUES (@nom, @prenom, @matricule, @fonction, @matricule_resp, @email, @compte_winds)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nom", demandeur.nom, DbType.String);
            parameters.Add("prenom", demandeur.prenom, DbType.String);
            parameters.Add("matricule", demandeur.matricule, DbType.String);
            parameters.Add("fonction", demandeur.fonction, DbType.String);
            parameters.Add("matricule_resp", demandeur.matricule_resp, DbType.String);
            parameters.Add("email", demandeur.email, DbType.String);
            parameters.Add("compte_winds", demandeur.compte_winds, DbType.String);
             
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createddemandeur = new demandeur
                {
                    demandeur_id = id,
                    nom = demandeur.nom,
                    prenom = demandeur.prenom,
                    matricule = demandeur.matricule,
                    fonction = demandeur.fonction,
                    matricule_resp = demandeur.matricule_resp,
                    email = demandeur.email,
                    compte_winds = demandeur.compte_winds,
                };
                return createddemandeur;
            }
        }

        public async Task UpdateDemandeur(int demandeur_id, DemandeurForUpdateDto demandeur)
        {
            var query = "UPDATE demandeur SET nom = @nom, prenom = @prenom, matricule = @matricule, fonction= @fonction, matricule_resp = @matricule_resp,  email= @email, compte_winds= @compte_winds   WHERE demandeur_id = @demandeur_id";

            var parameters = new DynamicParameters();
            parameters.Add("demandeur_id", demandeur_id, DbType.Int32);
            parameters.Add("nom", demandeur.nom, DbType.String);
            parameters.Add("prenom", demandeur.prenom, DbType.String);
            parameters.Add("matricule", demandeur.matricule, DbType.String);
            parameters.Add("fonction", demandeur.fonction, DbType.String);
            parameters.Add("matricule_resp", demandeur.matricule_resp, DbType.String);

            parameters.Add("email", demandeur.email, DbType.String);
            parameters.Add("compte_winds", demandeur.compte_winds, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteDemandeur(int demandeur_id)
        {
            var query = "DELETE FROM demandeur WHERE demandeur_id = @demandeur_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { demandeur_id });
            }
        }

        

        
    }

   

}
