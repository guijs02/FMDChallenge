using FMDApplication.Dtos.Participant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDApplication.Dtos.Lecture
{
    public class GetAllLectureDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IEnumerable<GetAllParticipantDto> Participants { get; set; } = new List<GetAllParticipantDto>();
    }
}
