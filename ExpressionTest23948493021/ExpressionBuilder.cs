using System;
using System.Linq;
using System.Linq.Expressions;

using static System.Linq.Expressions.Expression;

namespace ExpressionTest23948493021
{
    public static class ExpressionBuilder
    {
        public static IQueryable<T> FilterDynamically<T>(this IQueryable<T> source, FilterOption options)
        {

            var param = Parameter(typeof(T), "p");

            // source.Where(p=> p.Name == "Abuzer")
            //  p => p.Name == "Abuzer" 



            var condition = options.IsRoot ? ParseNested<T>(options, param) : GetInnerCondition(options, param);

            Expression<Func<T, bool>> lamda = Lambda<Func<T, bool>>(condition, param);
            // var compiled = lamda.Compile();


            return source.Where(lamda);
        }

        private static BinaryExpression GetInnerCondition(FilterOption options, ParameterExpression param)
        {
            var property = GetNestProperties(param, options.Field);

            var variable = Constant(options.Value);

            var condition = GetConditionExpression(options.Condition, property, variable);
            return condition;
        }
        private static MemberExpression GetNestProperties(ParameterExpression param, string propertyName)
        {
            if (!propertyName.Contains("."))
            {
                return Property(param, propertyName);
            }

            var props = propertyName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            var baseExpression = Property(param, props[0]);

            for (int i = 1; i < props.Length; i++)
            {
                baseExpression = Property(baseExpression, props[i]);
            }

            return baseExpression;
        }

        private static BinaryExpression ParseNested<T>(FilterOption options, ParameterExpression param)
        {
            var joinCondition = options.JoinCondition;
            var start = options.Groups.First();
            var condition = start.Groups.Count > 0 ? ParseNested<T>(start, param) : GetInnerCondition(start, param);

            for (int i = 1; i < options.Groups.Count; i++)
            {
                var current = options.Groups[i];
                var right = current.Groups.Count > 0 ? ParseNested<T>(current, param) : GetInnerCondition(current, param); ;
                condition = joinCondition == JoinCondition.And ? AndAlso(condition, right) : OrElse(condition, right);
            }

            return condition;
        }

        private static BinaryExpression GetConditionExpression(Condition condition, Expression left, Expression right) =>

             condition switch
             {
                 Condition.Equal => Equal(left, right),
                 Condition.GreaterThan => GreaterThan(left, right),
                 Condition.GreaterThanEqual => GreaterThanOrEqual(left, right),
                 Condition.LessThan => LessThan(left, right),
                 Condition.LessThanOrEqual => LessThanOrEqual(left, right),
                 Condition.NotEqual => NotEqual(left, right),
                 _ => throw new NotImplementedException(),
             };


    }
}
