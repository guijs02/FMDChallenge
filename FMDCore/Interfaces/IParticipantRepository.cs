using FMDInfra.Models;

namespace FMDCore.Interfaces
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> GetAllAsync();
        Task<Participant> AddAsync(Participant participante);
        Task<Participant?> UpdateAsync(Participant participant);
        Task<bool> DeleteAsync(Guid id);

    }
}
