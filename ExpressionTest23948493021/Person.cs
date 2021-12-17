using System;

namespace ExpressionTest23948493021
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public bool HasPortfoilo { get; set; }

        public DateTime CreateDate { get; set; }

        public Classifications Details { get; set; }
    }

    public class Classifications
    {
        public string Stage { get; set; }

        public string TrackType { get; set; }
    }
}
