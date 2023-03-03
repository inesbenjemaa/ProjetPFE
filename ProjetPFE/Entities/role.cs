using ProjetPFE.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjetPFE.Entities;

public class role
    
{
    
    public int role_id { get; set; } 

    public int demandeur_id { get; set; }


    [EnumDataType(typeof(nom_role))]
    public nom_role nom_role { get; set; }

}
