using System.Reflection.Metadata.Ecma335;

namespace CityInfo.API.Models
{
    public class PointOfIntrestDto
    {
        public int Id { get; set; }
        public string Name  { get; set; }=string.Empty;
        public string? Description { get; set; }
        public string cityName { get; set; }

        public string Institution { get; set; }
    }
}
