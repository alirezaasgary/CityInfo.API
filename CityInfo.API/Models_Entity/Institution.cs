using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models_Entity
{
    public class Institution
    {

        [StringLength(500)]
        public string Name { get; set; }

        public bool IsPublic { get; set; }
    }
}
