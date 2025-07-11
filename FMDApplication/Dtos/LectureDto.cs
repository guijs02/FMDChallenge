namespace FMDApplication.Dtos
{
    public class LectureDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IEnumerable<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    }
}
