namespace ProjetPFE.Entities;

public class employe
{
    public int employe_id { get; set; }
    public string? nom { get; set; }
    public string? prenom { get; set; }
    public string? matricule { get; set; }
    public string? matricule_resp { get; set; }
    public string? fonction { get; set; }
    public string? role { get; set; }
    public DateTime date_recrutement { get; set; }
    public string? email { get; set; }
    public string? compte_winds { get; set; }
    public ICollection<diplome> diplomes { get; set; }


}

