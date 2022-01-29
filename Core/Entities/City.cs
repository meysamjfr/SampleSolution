using System.Collections.Generic;
using Project.Entities.Base;

namespace Project.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Advert> Adverts { get; set; }
    }
}