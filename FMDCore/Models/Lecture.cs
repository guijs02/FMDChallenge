namespace FMDInfra.Models
{
    public class Lecture
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}
