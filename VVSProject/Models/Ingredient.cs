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
        public Ingredient(int id, string name, int quantity)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
        }
    }
}