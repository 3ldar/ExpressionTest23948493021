using System;
using System.Linq;

namespace ExpressionTest23948493021
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var collection = new[] {
                new Person { Name = "Abuzer", Age = 10, CreateDate = DateTime.Now , Details = new Classifications { Stage = "Lead" } } ,
                new Person { Name = "Mahmut", Age = 20, CreateDate = DateTime.Now , Details = new Classifications { Stage = "Lead" }} ,
                new Person { Name = "Buro", Age = 30, CreateDate = DateTime.Now , Details = new Classifications { Stage = "Registered" }} ,
                new Person { Name = "Muro", Age = 40, CreateDate = DateTime.Now , Details = new Classifications { Stage = "Registered" }} ,
            };

            var queryable = collection.AsQueryable();
            var filter = new FilterOption { Condition = Condition.Equal, Field = "Details.Stage", Value = "Lead" };
            var filter2 = new FilterOption
            {
                IsRoot = true,
                JoinCondition = JoinCondition.Or,
                Groups = new FilterOption[] {
                    new FilterOption { JoinCondition = JoinCondition.And,
                        Groups = new FilterOption[] {
                              new FilterOption {
                                   Condition = Condition.Equal, Field="Name", Value ="Abuzer"
                              },
                              new FilterOption { Condition = Condition.GreaterThan, Field= "Age", Value = 5}
                        }
                    },new FilterOption { Condition = Condition.LessThan,Field = "Age",Value =30}

                }
            };

            var selection = new[] { "Name", "Age" };
            var result = queryable.FilterDynamically(filter2);

            Console.WriteLine("Hello World!");
        }
    }
}
