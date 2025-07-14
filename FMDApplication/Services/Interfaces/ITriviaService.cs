using FMDApplication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDApplication.Services.Interfaces
{
    public interface ITriviaService
    {
        Task<TriviaApiResponse> GetTriviaAsync();
    }
}
