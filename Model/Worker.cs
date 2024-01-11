using System.Data;

namespace Model
{
    public class Worker
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}

        public string ? Position { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Position}";
            
        }
    }
}
