using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCafe.Models
{
    public class Statistic
    {
        [Key]
        public int id { get; set; }
        public double totalProfit { get; set; }
        public double dailyProfit { get; set; }

        [ForeignKey("Drink")]
        public int idDrink { get; set; }

        public Drink Drink { get; set; }
        public int noOfEmployees { get; set; }

        public Statistic() { }
    }
}
