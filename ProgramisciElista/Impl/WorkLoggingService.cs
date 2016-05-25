using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.WorkTime;
using Data;
using Data.Converters;
using ProgramisciElista.Interfaces;

namespace ProgramisciElista.Impl
{
    public class WorkLoggingService:IWorkLoggingService
    {
        private readonly IPlansDiaryService _plansDiaryService;

        public WorkLoggingService(IPlansDiaryService plansDiaryService)
        {
            _plansDiaryService = plansDiaryService;
        }


        public void StartLogWork(int userId)
        {
            using (var db = new ElistaDbContext())
            {
                
                db.WorkTimes.Add(new WorkTime()
                {
                    User = db.Users.Find(userId),
                    HourStart = DateTime.Now,
                    HourEnd = null,
                    IsLate = _plansDiaryService.IsLate(userId, DateTime.Now),
                    WorkLog = ""
                });

                db.SaveChanges();
            }
        }

        public void LogWork(string msg,int userId)
        {
            using (var db = new ElistaDbContext())
            {
                var workingOn = db.WorkTimes.First(x => x.HourEnd == null&&x.User.Id==userId);
                workingOn.HourEnd= DateTime.Now;
                workingOn.WorkLog = msg;

                db.WorkTimes.AddOrUpdate(x=>x.Id,workingOn);

                db.SaveChanges();

                db.WorkTimes.Add(new WorkTime()
                {
                    User = workingOn.User,
                    HourStart = DateTime.Now,
                    HourEnd = null,
                    IsLate = false,
                    WorkLog = ""
                });

                db.SaveChanges();
            }
        }

        public void EndLogWork(int userId, string msg)
        {
            using (var db = new ElistaDbContext())
            {
                var workingOn = db.WorkTimes.First(x => x.HourEnd == null && x.User.Id == userId);
                workingOn.HourEnd = DateTime.Now;
                workingOn.WorkLog = msg;

                db.WorkTimes.AddOrUpdate(x => x.Id, workingOn);

                db.SaveChanges();
            }
        }

        public Dictionary<int,List<WorkTimeDto>> GetMonthlyLogWork(int userId,DateTime from)
        {
            var dateTo = from.AddMonths(1);
            using (var db = new ElistaDbContext())
            {
                return db.WorkTimes
                    .Where(x => x.User.Id == userId && x.HourStart < dateTo && x.HourStart > from)
                    .GroupBy(x => x.HourStart.Day)
                    .ToDictionary(x => x.Key, x => x.Select(y=>y.MapToDto()).ToList());
            }
        }
    }
}
