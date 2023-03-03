using ProjetPFE.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjetPFE.Entities
{
    public class statut_demande
    {
        public int statut_id { get; set; }


        [EnumDataType(typeof(statut))]
        public statut statut { get; set; }


        public int demande_id { get; set; }
        public int validation_id { get; set; }

    }
}
