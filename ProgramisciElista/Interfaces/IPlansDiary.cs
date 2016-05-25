using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.PlansDiary;

namespace ProgramisciElista.Interfaces
{
    public interface IPlansDiaryService
    {
        PlansDiaryDto CreateUserDiary(int userId, DateTime start, DateTime end, DayOfWeek day, string jobInfo);

        void DeactivateUserDiary(int diaryId);

        Dictionary<DayOfWeek, List<PlansDiaryDto>> GetDiaryFor(int userId);

        bool IsLate(int userId, DateTime jobStart);
    }
}
