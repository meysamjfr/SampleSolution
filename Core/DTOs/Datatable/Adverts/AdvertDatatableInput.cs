using Project.DTOs.Datatable.Base;

namespace Project.DTOs.Datatable.Adverts
{
    public class AdvertDatatableInput : DatatableInput
    {
        public string Title { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
    }
}
