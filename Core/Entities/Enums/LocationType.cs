using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Enums
{
    public enum LocationType
    {
        [Display(Name = "شهری")]
        InCity,
        [Display(Name = "روستایی / درون بافت")]
        InVillage,
        [Display(Name = "روستایی / خارج بافت")]
        OutsideVillage,
        [Display(Name = "روستایی / چسبیده به بافت")]
        BesideVillage
    }
}
