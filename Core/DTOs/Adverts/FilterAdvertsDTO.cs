using Project.Entities.Enums;

namespace Project.DTOs.Adverts
{
    public class FilterAdvertsDTO
    {
        public string Search { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? Radius { get; set; }
        public FilterAdvertSortType? SortType { get; set; }
    }
}
