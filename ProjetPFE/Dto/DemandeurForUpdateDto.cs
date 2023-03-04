using ProjetPFE.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjetPFE.Dto
{
    public class DemandeurForUpdateDto
    {
        public string? nom { get; set; }
        public string? prenom { get; set; }
        public string? matricule { get; set; }
        public string? fonction { get; set; }
        public string? matricule_resp { get; set; }
       
        public string? email { get; set; }
        public string? compte_winds { get; set; }
       
        public nom_role nom_role { get; set; }
    }
}
