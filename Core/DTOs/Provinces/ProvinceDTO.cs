using System.Collections.Generic;
using Project.DTOs.Base;
using Project.DTOs.Cities;

namespace Project.DTOs.Provinces
{
    public class ProvinceDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<CityDTO> Cities { get; set; }
    }
}
