namespace ProjetPFE.Entities;

public class demandeur
{
    public int demandeur_id { get; set; }
    public string? nom { get; set; }
    public string? prenom { get; set; }
    public string? matricule { get; set; }
    public string? fonction { get; set; }
    public string? matricule_resp { get; set; }
    public string? email { get; set; }


    public List<role> roles { get; set; }   = new List<role>();
    public List<compte> comptes { get; set; } = new List<compte>();
    public List<demande> demandes { get; set; } = new List<demande>();
    public List<valid_dem> validations { get; set; } = new List<valid_dem>();
}
