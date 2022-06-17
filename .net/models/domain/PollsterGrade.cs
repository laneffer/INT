using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class PollsterGrade
    {
        public int PollsterId { get; set; }
        public string Pollster { get; set; }
        public int CandidateId { get; set; }
        public int TotalPolls { get; set; }
        public decimal AVGPCT { get; set; }
        public decimal Diff { get; set; }
    }
}
