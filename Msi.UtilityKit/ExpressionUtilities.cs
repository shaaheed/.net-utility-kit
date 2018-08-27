using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Msi.UtilityKit
{
    public static class ExpressionUtilities
    {

        private static readonly MethodInfo LambdaMethod = typeof(Expression).GetMethods().First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2);

        private static MethodInfo[] QueryableMethods = typeof(Queryable).GetMethods().ToArray();

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

        public static MemberExpression GetMemberInfo(this Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;
            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpr =
                        ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpr = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        public static MethodInfo GetLambdaFuncBuilder(this Type source, Type dest)
        {
            var predicateType = typeof(Func<,>).MakeGenericType(source, dest);
            return LambdaMethod.MakeGenericMethod(predicateType);
        }

        public static PropertyInfo GetPropertyInfo<T>(this string name) => typeof(T).GetProperties().Single(p => p.Name == name);

        public static ParameterExpression Parameter<T>()
            => Expression.Parameter(typeof(T));

        public static MemberExpression GetPropertyExpression(this ParameterExpression obj, PropertyInfo property)
            => Expression.Property(obj, property);

        public static LambdaExpression GetLambda<TSource, TDest>(this ParameterExpression obj, Expression arg)
            => GetLambda(typeof(TSource), typeof(TDest), obj, arg);

        public static LambdaExpression GetLambda(this Type source, Type dest, ParameterExpression obj, Expression arg)
        {
            var lambdaBuilder = GetLambdaFuncBuilder(source, dest);
            return (LambdaExpression)lambdaBuilder.Invoke(null, new object[] { arg, new[] { obj } });
        }

        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> query, LambdaExpression predicate)
        {
            var whereMethodBuilder = QueryableMethods
                .First(x => x.Name == "Where" && x.GetParameters().Length == 2)
                .MakeGenericMethod(new[] { typeof(T) });

            return (IQueryable<T>)whereMethodBuilder
                .Invoke(null, new object[] { query, predicate });
        }

        public static IQueryable<TEntity> CallOrderByOrThenBy<TEntity>(this IQueryable<TEntity> modifiedQuery, bool useThenBy, bool descending, Type propertyType, LambdaExpression keySelector)
        {
            var methodName = "OrderBy";
            if (useThenBy) methodName = "ThenBy";
            if (descending) methodName += "Descending";

            var method = QueryableMethods
                .First(x => x.Name == methodName && x.GetParameters().Length == 2)
                .MakeGenericMethod(new[] { typeof(TEntity), propertyType });

            return (IQueryable<TEntity>)method.Invoke(null, new object[] { modifiedQuery, keySelector });
        }

    }
}
