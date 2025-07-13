using FMDApplication.Dtos.Participant;

namespace FMDApplication.Dtos.Lecture
{
    public class CreateLectureInputDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IEnumerable<CreateParticipantLectureInputDto> Participants { get; set; } = new List<CreateParticipantLectureInputDto>();
    }
    public class CreateParticipantLectureInputDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class CreateParticipantLectureOutputDto
    {
        public Guid Id { get; set; }
        public Guid LectureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
    public class CreateLectureOutuputDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IEnumerable<CreateParticipantLectureOutputDto> Participants { get; set; } = new List<CreateParticipantLectureOutputDto>();
    }
}
