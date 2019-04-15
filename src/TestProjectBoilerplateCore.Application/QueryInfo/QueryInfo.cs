using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Abp.Domain.Entities;

namespace TestProjectBoilerplateCore.QueryInfo
{
    public class QueryInfo
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SearchText { get; set; }
        public List<string> SearchProperties { get; set; }
        public List<SortInfo> Sorters = new List<SortInfo>();
        public FilterInfo Filter { get; set; }

        //public BinaryExpression GetWhereExpression<TEntity>(ParameterExpression param, string oper, string propName,
        //    ConstantExpression konstant)
        //{

        //}

        //Binarna ekspresija za int : eq, lt, lte, gt, gte
        public BinaryExpression GetBinExpInt(string op, Expression prop, ConstantExpression konst)
        {
            switch (op.ToLower())
            {
                case "eq":
                    return Expression.Equal(prop, konst);
                case "lt":
                    return Expression.LessThan(prop, konst);
                case "lte":
                    return Expression.LessThanOrEqual(prop, konst);
                case "gt":
                    return Expression.GreaterThan(prop, konst);
                case "gte":
                    return Expression.GreaterThanOrEqual(prop, konst);
                default:
                    throw new InvalidOperationException("Unijeli ste nedefinisan operator.");
            }
        }

        //Binarna ekspresija za string: jednako - EQ, sadrzi - CT i pocinje sa - SW
        public BinaryExpression GetBinExpString(string op, Expression prop, ConstantExpression konst)
        {
            var konstEkspresija = Expression.Constant(true, typeof(bool));
            BinaryExpression bin;

            switch (op.ToLower())
            {
                case "eq":
                    return Expression.Equal(prop, konst);
                case "ct":
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] {typeof(string)});
                    MethodCallExpression contains = Expression.Call(prop, containsMethod, konst);
                    bin = Expression.Equal(contains, konstEkspresija);
                    break;
                case "sw":
                    MethodInfo startWithMethod = typeof(string).GetMethod("StartsWith", new[] {typeof(string)});
                    MethodCallExpression startsWith = Expression.Call(prop, startWithMethod, konst);
                    bin = Expression.Equal(startsWith, konstEkspresija);
                    break;

                default:
                    throw new InvalidOperationException("Unijeli ste nedefinisan operator.");
            }

            return bin;
        }

        //OrderBy ekspresija
        public Expression<Func<TEntity, object>> OrderExp<TEntity>(ParameterExpression param, SortInfo sort)
        {
            var propExp = Expression.Property(param, sort.Property);
            var konvert = Expression.Convert(propExp, typeof(object));
            var lambda = Expression.Lambda<Func<TEntity, object>>(konvert, param);
            return lambda;
        }

        //AndOr ekpsresija
        public Expression AndOrExpresion(ParameterExpression param, FilterInfo filter)
        {
            var tacno = Expression.Constant(true, typeof(bool));
            var netacno = Expression.Constant(false, typeof(bool));

            Expression andOr = filter.Condition == "and" ? tacno : netacno;

            BinaryExpression binarna;

            // treba razviti logiku

            return andOr;
        }

        //QueriInfo da se spoji kompletna eksprsija...
    }
}
