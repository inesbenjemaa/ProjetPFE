using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Entities;
using Dapper;
using System.Data;
using ProjetPFE.Dto;
using ProjetPFE.Helpers;

namespace ProjetPFE.Repository
{
    public class DemandeRepository : IDemandeRepository
    {
        private readonly DapperContext _context;
        private readonly IStatutRepository statutRepository;
      
        public DemandeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<demande>> Getdemandes()
        {
            var query = "SELECT * FROM demande";

            using (var connection = _context.CreateConnection())
            {
                var demandes = await connection.QueryAsync<demande>(query);
                return demandes.ToList();
            }
        }
        public async Task<demande> GetDemande(int demande_id)
        {
            var query = "SELECT * FROM demande WHERE demande_id = @demande_id";

            using (var connection = _context.CreateConnection())
            {
                var demande = await connection.QuerySingleOrDefaultAsync<demande>(query, new { demande_id });

                return demande;
            }
        }
        public async Task<demande> CreateDemande(DemandeForCreationDto demande, IFormFile file)
        {
            var query = "INSERT INTO demande (nbr_poste,  nb_a, type_dem, titre_fonction, lien_fichier, nom_fichier, mission, remarque, nature_contrat, fonction, collaborateur_remp, creation_date, affectation, diplome ) VALUES (@nbr_poste,  @nb_a, @type_dem, @titre_fonction, @lien_fichier, @nom_fichier, @mission, @remarque, @nature_contrat, @fonction, @collaborateur_remp, @creation_date, @affectation, @diplome)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nbr_poste", demande.nbr_poste, DbType.Int32);
            parameters.Add("nb_a", demande.nb_a, DbType.Int32);
            parameters.Add("type_dem", demande.type_dem, DbType.String);
            parameters.Add("titre_fonction", demande.titre_fonction, DbType.String);
            parameters.Add("lien_fichier", demande.lien_fichier, DbType.String);
            parameters.Add("nom_fichier", demande.nom_fichier, DbType.String);
            parameters.Add("mission", demande.mission, DbType.String);
            parameters.Add("remarque", demande.remarque, DbType.String);
            parameters.Add("nature_contrat", demande.nature_contrat, DbType.String);
            parameters.Add("fonction", demande.fonction, DbType.String);
            parameters.Add("collaborateur_remp", demande.collaborateur_remp, DbType.String);
            parameters.Add("creation_date", demande.creation_date, DbType.DateTime);
            parameters.Add("affectation", demande.affectation, DbType.String);
            parameters.Add("diplome", demande.diplome, DbType.String);




            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                statut_demande statut_demande = new statut_demande 
                { 
                    demande_id= id, 
                    statut_demandeur = statut.en_cours,
                    statut_chef = statut.en_attente,
                    statut_rh = statut.en_cours,
                    statut_ds = statut.en_cours
                };
                statutRepository.CreateStatut(statut_demande);


                var createddemande = new demande
                {
                    demande_id = id,
                    nbr_poste = demande.nbr_poste,
                    nb_a = demande.nb_a,
                    type_dem = demande.type_dem,
                    titre_fonction = demande.titre_fonction,
                    lien_fichier = demande.lien_fichier,
                    nom_fichier = demande.nom_fichier,
                    mission = demande.mission,
                    remarque = demande.remarque,
                    nature_contrat = demande.nature_contrat,
                    fonction = demande.fonction,
                    collaborateur_remp = demande.collaborateur_remp,
                    creation_date = demande.creation_date,
                    affectation = demande.affectation,
                    diplome = demande.diplome,

                };
                return createddemande;
            }
        }

        public async Task UpdateDemande(int demande_id, DemandeForUpdateDto demande)
        {
            var query = "UPDATE demande SET nbr_poste = @nbr_poste,  nb_a = @nb_a, type_dem = @type_dem, titre_fonction = @titre_fonction, lien_fichier = @lien_fichier, nom_fichier = @nom_fichier, mission = @mission, remarque = @remarque, nature_contrat = @nature_contrat, fonction = @fonction, collaborateur_remp = @collaborateur_remp, creation_date = @creation_date, affectation = @affectation, diplome = @diplome  WHERE demande_id = @demande_id";

            var parameters = new DynamicParameters();
            parameters.Add("demande_id", demande_id, DbType.Int32);
            parameters.Add("nbr_poste", demande.nbr_poste, DbType.Int32);
            parameters.Add("nb_a", demande.nb_a, DbType.Int32);
            parameters.Add("type_dem", demande.type_dem, DbType.String);
            parameters.Add("titre_fonction", demande.titre_fonction, DbType.String);
            parameters.Add("lien_fichier", demande.lien_fichier, DbType.String);
            parameters.Add("nom_fichier", demande.nom_fichier, DbType.String);
            parameters.Add("mission", demande.mission, DbType.String);
            parameters.Add("remarque", demande.remarque, DbType.String);
            parameters.Add("nature_contrat", demande.nature_contrat, DbType.String);
            parameters.Add("fonction", demande.fonction, DbType.String);
            parameters.Add("collaborateur_remp", demande.collaborateur_remp, DbType.String);
            parameters.Add("creation_date", demande.creation_date, DbType.DateTime);
            parameters.Add("affectation", demande.affectation, DbType.String);
            parameters.Add("diplome", demande.diplome, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteDemande(int demande_id)
        {
            var query = "DELETE FROM demande WHERE demande_id = @demande_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { demande_id });
            }
        }

       


    }
}
