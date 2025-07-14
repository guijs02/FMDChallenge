using FMDInfra.Models;

namespace FMDCore.Interfaces
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> GetAllAsync(int pageNumber, int pageSize);
        Task<Participant> AddAsync(Participant participante);
        Task<Participant?> UpdateAsync(Participant participant);
        Task<bool> DeleteAsync(Guid id);

    }
}
