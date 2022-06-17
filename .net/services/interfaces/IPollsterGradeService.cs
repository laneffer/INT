using Sabio.Models.Domain;
using System.Collections.Generic;

namespace Sabio.Services
{
    public interface IPollsterGradeService
    {
        List<PollsterGrade> GetByElectionId(int electionId);
    }
}
