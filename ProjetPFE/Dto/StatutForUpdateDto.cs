using ProjetPFE.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjetPFE.Dto
{
    public class StatutForUpdateDto
    {

        //public int statut_id { get; set; }
        
        public statut statut { get; set; }
        public int demande_id { get; set; }
        public int validation_id { get; set; }
    }
}
