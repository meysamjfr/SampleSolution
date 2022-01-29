using Project.DTOs.Base;

namespace Project.DTOs.Cities
{
    public class CityDTO : BaseDTO
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }
    }
}