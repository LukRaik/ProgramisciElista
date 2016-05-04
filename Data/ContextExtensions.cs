using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class ContextExtensions
    {
        public static void Update<T>(this ElistaDbContext db, Func<T,bool> where, Expression<Func<T, object>> property,object value) where T : class
        {
            var member = property.Body as MemberExpression;
            var prop = member?.Member as PropertyInfo;
            if (prop != null)
            {
                foreach (var obj in db.Set<T>().Where(where).ToList())
                {
                    prop.SetValue(obj,value);
                    db.Set<T>().Attach(obj);
                    var entry = db.Entry(obj);
                    entry.State = EntityState.Modified;
                    entry.Property(property).IsModified = true;
                }
            }
        }
    }
}
