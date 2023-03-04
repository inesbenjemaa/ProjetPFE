using ProjetPFE.Helpers;
using System.ComponentModel.DataAnnotations;

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

    public string? compte_winds { get; set; }


    [EnumDataType(typeof(nom_role))]
    public nom_role nom_role { get; set; }


    public List<demande> demandes { get; set; } = new List<demande>();
    public List<valid_dem> validations { get; set; } = new List<valid_dem>();
}
