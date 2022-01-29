using System.Collections.Generic;
using Project.Entities.Base;

namespace Project.Entities
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}