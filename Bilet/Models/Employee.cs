using Bilet.Models.Base;

namespace Bilet.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public int PositionId { get;set; }
        public Position Position { get; set; }
    }
}
