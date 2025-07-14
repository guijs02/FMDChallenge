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
        public async Task<IEnumerable<Participant>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Participants
                .Include(p => p.Lecture);

            var participants = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return participants;
        }

        public async Task<Participant> AddAsync(Participant participant)
        {
            if (!await _context.Lectures.AnyAsync(l => l.Id == participant.LectureId))
                return null;

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
            return participant;
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
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null) return false;

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
