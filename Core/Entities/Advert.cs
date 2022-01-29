using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using Project.Entities.Base;
using Project.Entities.Enums;

namespace Project.Entities
{
    public class Advert : BaseEntity
    {
        #region core
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Point Location { get; set; }
        public int Hits { get; set; } = 0;
        public double Price { get; set; }
        public ICollection<AdvertImage> Images { get; set; }
        public string VoiceUrl { get; set; }
        public string VideoUrl { get; set; }
        public string PlanUrl { get; set; }
        public string TridimensionalUrl { get; set; }
        public SellingType SellingType { get; set; }
        public EstateType EstateType { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public LocationType LocationType { get; set; }
        public SellerType SellerType { get; set; }
        #endregion

        #region not required for all
        public int? Area { get; set; }
        public int? BuildYear { get; set; }
        public int? ParkingSpaces { get; set; }
        public int? Rooms { get; set; }
        public int? Elevator { get; set; }
        public int? Masters { get; set; }
        public int? StoreRoomArea { get; set; }
        public bool? Laundry { get; set; }
        public bool? SwimmingPool { get; set; }
        public bool? Gym { get; set; }
        public bool? Lobby { get; set; }

        public UsageType? UsageType { get; set; }

        public DateTime? BuildStartDate { get; set; }
        public DateTime? BuildCompleteDate { get; set; }

        public double? PriceRent { get; set; }
        #endregion
    }
}