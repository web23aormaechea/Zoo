using System.ComponentModel.DataAnnotations;

namespace Zoo.Models
{
    public class Arraza
    {
        [Key]
        public int ID { get; set; }
        public string Izena { get; set; }
        public string? Mota { get; set;}
        public string? ImageUrl { get; set; }
        public int? ID_lekua { get; set; }
    }
}
