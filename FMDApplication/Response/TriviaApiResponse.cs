using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDApplication.Response
{
    public struct TriviaApiResponse
    {
        public List<TriviaResult> Results { get; set; }
    }
    public struct TriviaResult
    {
        public string Question { get; set; } 
        public string Correct_Answer { get; set; }
    }
}
