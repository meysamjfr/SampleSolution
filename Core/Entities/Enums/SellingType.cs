using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Enums
{
    public enum SellingType
    {
        [Display(Name = "فروش")]
        Sale,
        [Display(Name = "اجاره")]
        Rent,
        [Display(Name = "پیش فروش")]
        Presell
    }
}
