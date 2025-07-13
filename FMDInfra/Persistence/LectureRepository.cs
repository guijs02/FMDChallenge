using FMDCore.Interfaces;
using FMDInfra.Data;
using FMDInfra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDInfra.Persistence
{
    public class LectureRepository : BaseRepository<Lecture>, ILectureRepository
    {
        private readonly AppDbContext _context;
        public LectureRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            return await _context.Lectures.Include(s => s.Participants).ToListAsync();
        }
    }
}
