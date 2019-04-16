using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Abp.Domain.Entities;
using Abp.UI;

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
            //mora ToUpper jer postoji problem sa slovima pojedinih stranih jezika npr Turskom
            switch (op.ToUpper())
            {
                case "EQ":
                    return Expression.Equal(prop, konst);
                case "LT":
                    return Expression.LessThan(prop, konst);
                case "LTE":
                    return Expression.LessThanOrEqual(prop, konst);
                case "GT":
                    return Expression.GreaterThan(prop, konst);
                case "GTE":
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

            switch (op.ToUpper())
            {
                case "EQ":
                    return Expression.Equal(prop, konst);
                case "CT":
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] {typeof(string)});
                    MethodCallExpression contains = Expression.Call(prop, containsMethod, konst);
                    bin = Expression.Equal(contains, konstEkspresija);
                    break;
                case "SW":
                    MethodInfo startWithMethod = typeof(string).GetMethod("StartsWith", new[] {typeof(string)});
                    MethodCallExpression startsWith = Expression.Call(prop, startWithMethod, konst);
                    bin = Expression.Equal(startsWith, konstEkspresija);
                    break;
                case "EW":
                    MethodInfo endWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                    MethodCallExpression endWith = Expression.Call(prop, endWithMethod, konst);
                    bin = Expression.Equal(endWith, konstEkspresija);
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
        public Expression AndOrExpresion<Tentity>(ParameterExpression param, List<RuleInfo> rules, string condition)
        {
            //true/false zadati 
            var tacno = Expression.Constant(true, typeof(bool));
            var netacno = Expression.Constant(false, typeof(bool));

            Expression paramExpression = param;
            Expression andOr = condition == "and" ? tacno : netacno;

            BinaryExpression binarna;

            foreach (var rule in rules)
            {
                //property expression
                Expression propExpression = Expression.Property(paramExpression, rule.Property);
                
                //tip property expression
                Type tip = propExpression.Type;

                var ruleValue = Convert.ChangeType(rule.Value, tip);

                //konstanta exkaspresija
                var valueToKonstanta = Expression.Constant(ruleValue);

                switch (ruleValue)
                {
                    case string _:
                        binarna = GetBinExpString(rule.Operator, propExpression, valueToKonstanta);
                        break;

                    case int _:
                        binarna = GetBinExpInt(rule.Operator, propExpression, valueToKonstanta);
                        break;
                    default:
                        throw new UserFriendlyException("Nedefinisan tip.");
                }
                //prvi uslov
                switch (condition.ToUpper())
                {
                    case "AND":
                        andOr = Expression.AndAlso(andOr, binarna);
                        break;
                    case "OR":
                        andOr = Expression.OrElse(andOr, binarna);
                        break;
                    default:
                        throw new UserFriendlyException("Nedefinisan condition.");
                }
                //pomogla mi Sofija ukoliko je Condition Null da se nastavi sa uslovom
                if (rule.Condition==null)
                {
                    continue;
                }
                switch (condition.ToUpper())
                {
                    case "AND":
                        andOr = Expression.AndAlso(andOr, AndOrExpresion<Tentity>(param, rule.Rules, rule.Condition));
                        break;
                    case "OR":
                        andOr = Expression.OrElse(andOr, AndOrExpresion<Tentity>(param, rule.Rules, rule.Condition));
                        break;
                    default:
                        throw new UserFriendlyException("Nedefinisan condition.");
                }

            }

            return andOr;
        }

        //kreiranje lambda expresije za Filter
        public Expression<Func<TEntity, bool>> FilterLambdaExpression<TEntity>(ParameterExpression param,
            Expression exp)
        {
            return Expression.Lambda<Func<TEntity, bool>>(exp, param);
        }

        //QueriInfo da se spoji kompletna eksprsija...

        public IQueryable<TEntity> QueriInfoExpression<TEntity>(QueryInfo query, List<TEntity> lista)
        {
            List<SortInfo> sort = query.Sorters;
            FilterInfo filter = query.Filter;
            List<RuleInfo> rules = filter.Rules;

            ParameterExpression param = Expression.Parameter(typeof(TEntity), "c");

            var filters = AndOrExpresion<TEntity>(param, rules, filter.Condition);

            var listaNew = lista.AsQueryable();

            listaNew = listaNew.Where(FilterLambdaExpression<TEntity>(param, filters));


            //zavrsiti QueryInfo 


            return listaNew;
        }

    }
}
