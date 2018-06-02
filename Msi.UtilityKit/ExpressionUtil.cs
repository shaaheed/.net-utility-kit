using System;
using System.Linq;
using System.Linq.Expressions;

namespace Msi.UtilityKit
{
    public static class ExpressionUtil
    {

        public static Expression<Func<T, bool>> New<T>(Expression<Func<T, bool>> expr = null)
        {
            if (expr == null) return x => false;
            return expr;
        }

        public static Expression<Func<T, bool>> New<T>(bool defaultExpression)
        {
            if (defaultExpression) return x => true;
            return x => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

    }
}
