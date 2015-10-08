namespace CarsStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Transmission
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
