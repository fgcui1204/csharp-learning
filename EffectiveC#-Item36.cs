using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace csharp_learning
{
    //理解查询表达式与方法调用之间的映射关系
    public class People
    {
        public readonly int Age;
        public readonly string LastName;
        public readonly string FirstName;


        public People(int age, string lastName, string firstName)
        {
            Age = age;
            LastName = lastName;
            FirstName = firstName;
        }

        public static List<People> getPeoples()
        {
            List<People> peoples = new List<People>
            {
                new People(22, "Wang", "Feng"),
                new People(18, "Cai", "Xukun"),
                new People(55, "Cai", "Ming"),
                new People(50, "Liu", "Huan"),
                new People(48, "Li", "Keqin")
            };
            return peoples;
        }
    }

    public class UnitTest36
    {
        [Fact]
        public void ShouldReturnNumbersWhenFilterGreater3()
        {
            var numbers = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            // var smallNumbers = from n in numbers
            //     where n < 3
            //     select n;

            // 可以省略Select，因为Select是要在上一条表达式的返回结果中做选择；.Select(n => n)
            var smallNumbers = numbers.Where(n => n < 3);

            Assert.Equal(new[] {0, 1, 2}, smallNumbers);
        }

        //Select 的应用场景：将输入的转换为其他元素或转换成另一种类型
        [Fact]
        public void ShouldReturnSquareValue()
        {
            var numbers = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            // var squareNumbers = from n in numbers
            //     where n < 3
            //     select n * n;

            var squareNumbers = numbers
                .Where(n => n < 3)
                .Select(n => n * n);
            Assert.Equal(new[] {0, 1, 4}, squareNumbers);


            var squaresFromQueryExpression = from n in numbers
                where n < 3
                select new {Number = n, Square = n * n};
            var squares = numbers
                .Where(n => n < 3)
                .Select(n => new {Number = n, Sqaure = n * n});

            // squaresFromQueryExpression.Should().AllBeAssignableTo(squares);
        }

        [Fact]
        public void ShouldReturnPresonWithExpectedOrder()
        {
            List<People> peoples = People.getPeoples();

            // var orderedPeople = from p in peoples
            //     where p.Age > 20
            //     orderby p.FirstName, p.LastName, p.Age
            //     select p;

            var orderedPeople = peoples.Where(p => p.Age > 20)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.Age);
            // IOrderedEnumerable<People> result = Enumerable.Empty<People>().OrderBy(x => x.Age);
            // Assert.Equal(result, orderedPeople);
        }
    }
}