using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Project.Entities.Enums;

namespace Project.DTOs.Adverts
{
    public class CreateAdvertDTO
    {
        [Required(ErrorMessage = "{0} را وارد نکرده اید")]
        [Display(Name = "تیتر")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} را وارد نکرده اید")]
        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "{0} را وارد نکرده اید")]
        [Display(Name = "lat")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "{0} را وارد نکرده اید")]
        [Display(Name = "lng")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "{0} را وارد نکرده اید")]
        [DataType(DataType.Currency, ErrorMessage = "{0} نامعتبر است")]
        [Display(Name = "مبلغ")]
        public double Price { get; set; }

        public string VoiceUrl { get; set; }

        public string VideoUrl { get; set; }

        public string PlanUrl { get; set; }

        public string TridimensionalUrl { get; set; }

        public SellingType SellingType { get; set; }

        public EstateType EstateType { get; set; }

        public int CityId { get; set; }

        public LocationType LocationType { get; set; }

        public SellerType SellerType { get; set; }
    }
}
