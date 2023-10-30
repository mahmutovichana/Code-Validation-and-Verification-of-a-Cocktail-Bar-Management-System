using System.ComponentModel.DataAnnotations;

namespace SmartCafe.Models
{
    public class Ingredient
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }

        public Ingredient() { }
    }
}
