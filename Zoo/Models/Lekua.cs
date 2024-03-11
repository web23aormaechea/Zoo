using System.ComponentModel.DataAnnotations;

namespace Zoo.Models
{
    public class Lekua
    {
        [Key]
        public int ID { get; set; }
        public string Izena { get; set; }
        public string? Deskribapena { get; set; }
        
    }
}
