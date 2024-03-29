﻿namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Descriptions { get; set; }

        //read only
        public int NumberOfpointOfIntrest { get {return PointOfIntrests.Count;} }

       public ICollection<PointOfIntrestDto> PointOfIntrests { get; set; } = new List<PointOfIntrestDto>();
    }
}
