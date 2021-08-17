using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace csharp_learning
{

    class Person
    {
        private int _income;
        public int Income
        {
            get => _income;
            set
            {
                if (value > 5)
                {
                    // stupid fake rule
                    throw new Exception("not allowed");
                }
                else
                {
                    _income = value;
                }
            }
        }

        public Person(int income)
        {
            Income = income;
        }
    }

    public class UnitTest39
    {
        [Fact]
        public void ModifyInPlaceMayLeaveDataInconsistent()
        {
            var people = Enumerable.Range(1, 5).Select(n => new Person(n)).ToList();

            var resultIncomeList = people.Select(p => p.Income).ToList();
            Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, resultIncomeList);

            people.ForEach(p =>
            {
                try
                {
                    p.Income = p.Income + 1;
                }
                catch (Exception ex) { }
            });

            resultIncomeList = people.Select(p => p.Income).ToList();
            Assert.Equal(new List<int> { 2, 3, 4, 5, 5 }, resultIncomeList);
        }

        [Fact]
        public void LazyEvalKeepsDataConsistent()
        {
            var people = Enumerable.Range(1, 5).Select(n => new Person(n));

            var resultIncomeList = people.Select(p => p.Income).ToList();
            Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, resultIncomeList);

            people.ToList().ForEach(p =>
            {
                try
                {
                    p.Income = p.Income + 1;
                }
                catch (Exception ex) { }
            });

            resultIncomeList = people.Select(p => p.Income).ToList();
            Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, resultIncomeList);
        }
    }
}
