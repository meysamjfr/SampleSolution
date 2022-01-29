using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Enums
{
    public enum EstateType
    {
        [Display(Name = "آپارتمان")]
        Apartment,
        [Display(Name = "واحد تجاری")]
        BusinessUnit,
        [Display(Name = "واحد اداری")]
        OfficeUnit,
        [Display(Name = "ویلا")]
        Villa,
        [Display(Name = "خانه کلنگی")]
        OldHouse,
        [Display(Name = "زمین / باغ")]
        Garden,
        [Display(Name = "واحد صنعتی / سوله / کارگاه")]
        IndustrialUnit
    }
}