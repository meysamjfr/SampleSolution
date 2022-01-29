using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Enums
{
    public enum UsageType
    {
        [Display(Name = "مسکونی")]
        Residential,
        [Display(Name = "صنعتی")]
        Industrial,
        [Display(Name = "اداری")]
        Official,
        [Display(Name = "تجاری")]
        Commercial,
        [Display(Name = "کشاورزی")]
        Agriculture
    }
}
