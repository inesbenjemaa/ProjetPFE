using ProjetPFE.Helpers;

namespace ProjetPFE.Dto
{
    public class StatutForCreationDto
    {
        //public int statut_id { get; set; }
        public statut statut { get; set; }
        public int demande_id { get; set; }
        public int validation_id { get; set; }
    }
}
