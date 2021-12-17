using System.Collections.Generic;

namespace ExpressionTest23948493021
{
    public class FilterOption
    {
        public string Field { get; set; }

        public object Value { get; set; }

        public Condition Condition { get; set; }

        public JoinCondition JoinCondition { get; set; }

        public bool IsRoot { get; set; }

        public IList<FilterOption> Groups { get; set; } = new List<FilterOption>();
    }

    public enum Condition
    {
        Equal,
        GreaterThan,
        GreaterThanEqual,
        LessThan,
        LessThanOrEqual,
        NotEqual,
    }

    public enum JoinCondition
    {
        And,
        Or,
    }
}
