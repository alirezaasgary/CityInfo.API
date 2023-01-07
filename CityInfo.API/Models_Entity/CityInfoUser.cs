namespace CityInfo.API.Models_Entity
{
    public class CityInfoUser
    {
        public CityInfoUser(int userId,string userName,string firstName, string lastName , string city )
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
    }
}
