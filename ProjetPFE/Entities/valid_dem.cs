using ProjetPFE.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjetPFE.Entities
{
    public class valid_dem
    {
        public int validation_id { get; set; }


        [EnumDataType(typeof(Validation))]
        public Validation validation { get; set;}


        public int demandeur_id { get; set;}
        public List<statut_demande> statuts { get; set; } = new List<statut_demande>();


    }
}
