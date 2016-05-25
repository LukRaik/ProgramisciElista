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
        public static void Update<T>(this ElistaDbContext db, Func<T, bool> where, Expression<Func<T, object>> property, object value) where T : class
        {

            MemberExpression Exp = null;
            
            if (property.Body is UnaryExpression)
            {
                var UnExp = (UnaryExpression)property.Body;
                if (UnExp.Operand is MemberExpression)
                {
                    Exp = (MemberExpression)UnExp.Operand;
                }
                else
                    throw new ArgumentException();
            }
            else if (property.Body is MemberExpression)
            {
                Exp = (MemberExpression)property.Body;
            }
            else
            {
                throw new ArgumentException();
            }

            var prop = (PropertyInfo)Exp.Member;
            if (prop != null)
            {
                foreach (var obj in db.Set<T>().Where(where).ToList())
                {
                    prop.SetValue(obj, value);
                    db.Set<T>().Attach(obj);
                    var entry = db.Entry(obj);
                    entry.State = EntityState.Modified;
                    entry.Property(property).IsModified = true;
                }
            }
        }
    }
}
