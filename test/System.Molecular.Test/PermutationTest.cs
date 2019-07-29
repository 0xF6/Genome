namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Molecular.Extra;
    using NUnit.Framework;

    public class PermutationTest
    {
        [Test]
        public void Test()
        {
            var n = new[] { "A", "C", "G", "U" };
            var result = GetPermutations(n, 3);
            foreach (var i in result)
                TestContext.Out.WriteLine(new Codon(i.Select(x => x.First())).ToFullBigString());
            TestContext.Out.WriteLine($"Count: {result.Count()}");
        }
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}