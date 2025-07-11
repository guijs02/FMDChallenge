namespace FMDApplication.Dtos
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public int LectureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
