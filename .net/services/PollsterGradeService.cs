using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class PollsterGradeService : IPollsterGradeService
    {
        IDataProvider _data = null;
        public PollsterGradeService(IDataProvider data)
        {
            _data = data;
        }
        public List<PollsterGrade> GetByElectionId(int electionId)
        {
            string procName = "[dbo].[PollstersPctDiff_SelectByElectionId]";

            List<PollsterGrade> pollGrade = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ElectionId", electionId);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                PollsterGrade aGrade = MapPollsterGrade(reader, ref startingIndex);

                if (pollGrade == null)
                {
                    pollGrade = new List<PollsterGrade>();
                }
                pollGrade.Add(aGrade);
            });

            return pollGrade;
        }
        private static PollsterGrade MapPollsterGrade(IDataReader reader, ref int startingIndex)
        {
            PollsterGrade aGrade = new PollsterGrade();

            startingIndex = 0;

            aGrade.PollsterId = reader.GetSafeInt32(startingIndex++);
            aGrade.Pollster = reader.GetSafeString(startingIndex++);
            aGrade.CandidateId = reader.GetSafeInt32(startingIndex++);
            aGrade.TotalPolls = reader.GetSafeInt32(startingIndex++);
            aGrade.AVGPCT = reader.GetSafeDecimal(startingIndex++);
            aGrade.Diff = reader.GetSafeDecimal(startingIndex++);

            return aGrade;
        }
    }
}
