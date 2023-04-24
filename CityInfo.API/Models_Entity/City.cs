using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Models_Entity
{
    public class City
    {
        public City(string name) // جهت اطمینان از اینکه نام شهر حتما پر شده باشد .
        {
            this.Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // اگر اسن ستون ای دی باشد نیازی به ای خط کد نیست
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Institution Affiliation { get; set; }

        public ICollection<PointOfIntrest> PointOfIntrests { get; set; } = new List<PointOfIntrest>();
    }
}
