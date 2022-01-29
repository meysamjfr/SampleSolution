using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Enums
{
    public enum SellerType
    {
        [Display(Name = "شخص")]
        Individual,
        [Display(Name = "املاک")]
        RealEstate
    }
}
