using Project.DTOs.Base;
using Project.Entities.Enums;

namespace Project.DTOs.Adverts
{
    public class AdvertDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Hits { get; set; }
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
