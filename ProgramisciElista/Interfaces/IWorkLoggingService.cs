using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.WorkTime;

namespace ProgramisciElista.Interfaces
{
    public interface IWorkLoggingService
    {
        void StartLogWork(int userId);

        void LogWork(string msg, int userId);

        void EndLogWork(int userId, string msg);

        Dictionary<int, List<WorkTimeDto>> GetMonthlyLogWork(int userId, DateTime from);
    }
}                    