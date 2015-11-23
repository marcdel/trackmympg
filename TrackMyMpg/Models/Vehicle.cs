using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyMpg.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public Make Make { get; set; }

        public decimal Mpg { get; set;}
    }
}