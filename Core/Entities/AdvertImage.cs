using Project.Entities.Base;

namespace Project.Entities
{
    public class AdvertImage : BaseEntity
    {

        public string ImageUrl { get; set; }
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}