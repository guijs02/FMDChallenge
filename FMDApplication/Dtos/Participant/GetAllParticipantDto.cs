using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDApplication.Dtos.Participant
{
    public class GetAllParticipantDto
    {
        public Guid Id { get; set; }
        public Guid LectureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
