using FMDCore.Interfaces;
using FMDInfra.Data;
using FMDInfra.Models;
using Microsoft.EntityFrameworkCore;

namespace FMDInfra.Persistence
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext _context;

        public ParticipantRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Participant>> GetAllAsync()
        {
            return await _context.Participants
                .Include(p => p.Lecture)
                .ToListAsync();
        }

        public async Task<Participant> AddAsync(Participant participante)
        {
            _context.Participants.Add(participante);
            await _context.SaveChangesAsync();
            return participante;
        }

        public async Task<Participant?> UpdateAsync(Participant participant)
        {
            var participantDb = await _context.Participants.FindAsync(participant.Id);
            if (participantDb == null) return null;

            participantDb.Name = participant.Name;
            participantDb.Email = participant.Email;
            participantDb.Phone = participant.Phone;
            participantDb.LectureId = participant.LectureId;

            await _context.SaveChangesAsync();

            return participantDb;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var participante = await _context.Participants.FindAsync(id);
            if (participante == null) return false;

            _context.Participants.Remove(participante);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
