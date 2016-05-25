using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.PlansDiary;
using Data;
using Data.Converters;
using ProgramisciElista.Interfaces;

namespace ProgramisciElista.Impl
{
    public class PlansDiaryService : IPlansDiaryService
    {

        public PlansDiaryDto CreateUserDiary(int userId, DateTime start, DateTime end, DayOfWeek day, string jobInfo)
        {
            using (var db = new ElistaDbContext())
            {
                var diary = db.PlansDiaries.Add(new PlansDiary()
                {
                    User = db.Users.Find(userId),
                    Day = day,
                    EndDate = end,
                    StartDate = start,
                    IsArchive = false,
                    Info = jobInfo
                });
                db.SaveChanges();

                return diary.MapToDto();
            }
        }

        public void DeactivateUserDiary(int diaryId)
        {
            using (var db = new ElistaDbContext())
            {
                db.Update<PlansDiary>(x => x.Id == diaryId, x => x.IsArchive, false);

                db.SaveChanges();
            }
        }

        public Dictionary<DayOfWeek, List<PlansDiaryDto>> GetDiaryFor(int userId)
        {
            using (var db = new ElistaDbContext())
            {
                return db.PlansDiaries.Where(x => x.User.Id == userId&&x.IsArchive==false).GroupBy(x => x.Day).OrderBy(x => x.Key)
                    .ToDictionary(x => x.Key, x =>
                    {
                        var list = x.Select(y => y.MapToDto()).OrderBy(y => y.StartDate);
                        return list.ToList();
                    });
            }
        }

        public bool IsLate(int userId, DateTime jobStart)
        {
            using (var db = new ElistaDbContext())
            {
                var job = db.PlansDiaries
                    .Where(x => x.IsArchive == false && x.User.Id == userId && x.Day==jobStart.DayOfWeek)
                    .OrderBy(x => x.StartDate)
                    .FirstOrDefault();

                if (job == null) return false;
                if (job.StartDate.Hour < jobStart.Hour) return true;
                if (job.StartDate.Hour == jobStart.Hour && job.StartDate.Minute < jobStart.Minute) return true;
                return false;
            }
        }
    }
}
