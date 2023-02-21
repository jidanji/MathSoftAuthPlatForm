using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MathSoftCommonLib
{
    #region 马良废弃此类
    //#region 两个Expression与或操作
    ///// <summary>
    ///// 两个Expression与或操作
    ///// </summary>
    //public static class PredicateBuilder
    //{

    //    /// <summary>
    //    /// 固定值true
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <returns></returns>
    //    public static Expression<Func<T, bool>> True<T>() { return f => true; }
    //    /// <summary>
    //    /// 固定值false
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <returns></returns>
    //    public static Expression<Func<T, bool>> False<T>() { return f => false; }

    //    #region 两个Expression做OR操作
    //    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
    //                                                        Expression<Func<T, bool>> expr2)
    //    {
    //        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
    //        return Expression.Lambda<Func<T, bool>>
    //              (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
    //    }
    //    #endregion

    //    #region 两个Expression做AND操作
    //    /// <summary>
    //    /// 两个Expression做AND操作
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="expr1"></param>
    //    /// <param name="expr2"></param>
    //    /// <returns></returns>
    //    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
    //                                                         Expression<Func<T, bool>> expr2)
    //    {
    //        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
    //        return Expression.Lambda<Func<T, bool>>
    //              (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
    //    }
    //    #endregion

    //}
    //#endregion
    #endregion

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public static class PredicateBuilder
    {

        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)  
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first  
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression   
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }

}
