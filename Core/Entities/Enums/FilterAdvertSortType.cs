using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Enums
{
    public enum FilterAdvertSortType
    {
        [Display(Name = "جدید ترین")]
        Recent,
        [Display(Name = "نزدیک ترین")]
        Nearby,
        [Display(Name = "محبوب ترین")]
        Popular,
        [Display(Name = "قیمت صعودی")]
        LowestPrice,
        [Display(Name = "قیمت نزولی")]
        HighestPrice,
    }
}