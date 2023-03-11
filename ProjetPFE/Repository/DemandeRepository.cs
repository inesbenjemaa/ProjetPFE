using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Dto;
using ProjetPFE.Entities;
using System.Data;

namespace ProjetPFE.Repository
{
    public class DemandeRepository : IDemandeRepository
    {
        private readonly DapperContext _context;

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

        
       

        public async Task<demande> CreateDemande(DemandeForCreationDto demande)
        {
            var query = "INSERT INTO demande (employe_id,  nb_a_exp, type_demande, titre_fonction, nature_contrat, lien_fichier, " +
                "nom_fichier, remarque, statut_chef, statut_rh, statut_ds, motif_chef, motif_rh, motif_ds, collaborateur_remp, " +
                "date_creation, affectation ) VALUES (@employe_id,  @nb_a_exp, @type_demande, @titre_fonction, @nature_contrat, " +
                "@lien_fichier, @nom_fichier, @remarque, @statut_chef, @statut_rh, @statut_ds, @motif_chef, @motif_rh, @motif_ds," +
                " @collaborateur_remp, @date_creation, @affectation)" + "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("employe_id", demande.employe_id, DbType.Int32);
            parameters.Add("nb_a_exp", demande.nb_a_exp, DbType.Int32);
            parameters.Add("type_demande", demande.type_demande, DbType.String);
            parameters.Add("titre_fonction", demande.titre_fonction, DbType.String);
            parameters.Add("nature_contrat", demande.nature_contrat, DbType.String);
            parameters.Add("lien_fichier", demande.lien_fichier, DbType.String);
            parameters.Add("nom_fichier", demande.nom_fichier, DbType.String);
            parameters.Add("remarque", demande.remarque, DbType.String);
            parameters.Add("statut_chef", demande.statut_chef, DbType.String);
            parameters.Add("statut_rh", demande.statut_rh, DbType.String);
            parameters.Add("statut_ds", demande.statut_ds, DbType.String);
            parameters.Add("motif_chef", demande.motif_chef, DbType.String);
            parameters.Add("motif_rh", demande.motif_rh, DbType.String);
            parameters.Add("motif_ds", demande.motif_ds, DbType.String);
            parameters.Add("collaborateur_remp", demande.collaborateur_remp, DbType.String);
            parameters.Add("date_creation", demande.date_creation, DbType.DateTime);
            parameters.Add("affectation", demande.affectation, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createddemande = new demande
                {
                    demande_id = id,
                    employe_id = demande.employe_id,
                    nb_a_exp = demande.nb_a_exp,
                    type_demande = demande.type_demande,
                    titre_fonction = demande.titre_fonction,
                    lien_fichier = demande.lien_fichier,
                    nom_fichier = demande.nom_fichier,
                    statut_chef = demande.statut_chef,
                    statut_rh = demande.statut_rh,
                    statut_ds = demande.statut_ds,
                    remarque = demande.remarque,
                    nature_contrat = demande.nature_contrat,
                    motif_chef = demande.motif_chef,
                    motif_rh = demande.motif_rh,
                    motif_ds = demande.motif_ds,
                    collaborateur_remp = demande.collaborateur_remp,
                    date_creation = demande.date_creation,
                    affectation = demande.affectation,

                };
                return createddemande;
            }
        }
    

    public async Task UpdateDemande(int demande_id, DemandeForUpdateDto demande)
    {
        var query = "UPDATE demande SET employe_id = @employe_id,  nb_a_exp = @nb_a_exp, type_demande = @type_demande, titre_fonction = @titre_fonction, " +
            "lien_fichier = @lien_fichier, nom_fichier = @nom_fichier, statut_chef = @statut_chef, statut_rh = @statut_rh," +
            " statut_ds = @statut_ds, remarque = @remarque," +
            " nature_contrat = @nature_contrat, motif_chef = @motif_chef, motif_rh = @motif_rh, motif_ds = @motif_ds," +
            " collaborateur_remp = @collaborateur_remp, " +
            "date_creation = @date_creation, affectation = @affectation WHERE demande_id = @demande_id";

        var parameters = new DynamicParameters();
        parameters.Add("employe_id", demande.employe_id, DbType.Int32);
        parameters.Add("nb_a_exp", demande.nb_a_exp, DbType.Int32);
        parameters.Add("type_demande", demande.type_demande, DbType.String);
        parameters.Add("titre_fonction", demande.titre_fonction, DbType.String);
        parameters.Add("nature_contrat", demande.nature_contrat, DbType.String);
        parameters.Add("lien_fichier", demande.lien_fichier, DbType.String);
        parameters.Add("nom_fichier", demande.nom_fichier, DbType.String);
        parameters.Add("remarque", demande.remarque, DbType.String);
        parameters.Add("statut_chef", demande.statut_chef, DbType.String);
        parameters.Add("statut_rh", demande.statut_rh, DbType.String);
        parameters.Add("statut_ds", demande.statut_ds, DbType.String);
        parameters.Add("motif_chef", demande.motif_chef, DbType.String);
        parameters.Add("motif_rh", demande.motif_rh, DbType.String);
        parameters.Add("motif_ds", demande.motif_ds, DbType.String);
        parameters.Add("collaborateur_remp", demande.collaborateur_remp, DbType.String);
        parameters.Add("date_creation", demande.date_creation, DbType.DateTime);
        parameters.Add("affectation", demande.affectation, DbType.String);


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
