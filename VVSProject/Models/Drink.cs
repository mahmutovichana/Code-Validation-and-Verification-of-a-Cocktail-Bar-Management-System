using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SmartCafe.Models
{
    public class Drink
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public Drink() { }

        public Drink(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
       
    }
}
