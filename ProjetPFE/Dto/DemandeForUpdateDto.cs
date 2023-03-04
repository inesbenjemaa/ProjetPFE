namespace ProjetPFE.Dto
{
    public class DemandeForUpdateDto
    {
        //public int demande_id { get; set; }
        public int nbr_poste { get; set; }
        public int demandeur_id { get; set; }
        public int nb_a { get; set; }
        public string? type_dem { get; set; }
        public string? titre_fonction { get; set; }
        public string? lien_fichier { get; set; }
        public string? nom_fichier { get; set; }
        public string? mission { get; set; }
        public string? remarque { get; set; }
        public string? nature_contrat { get; set; }
        public string? fonction { get; set; }
        public string? collaborateur_remp { get; set; }
        public DateTime creation_date { get; set; }
        public string? affectation { get; set; }
        public string? diplome { get; set; }
        public int num_arch { get; set; }
    }
}
