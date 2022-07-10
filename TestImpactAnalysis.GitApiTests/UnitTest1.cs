using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestImpactAnalysis.GitApi;

namespace TestImpactAnalysis.GitApiTests
{
    [TestClass]
    public class UnitTest1
    {
        private string ThisRepo = @"C:\Airport\Programming\Projects\TestImpactAnalysis.GitApi";
        private string DemoRepo = @"C:\Airport\Programming\Projects\TestImpactAnalysis.DemoSolution";

        [TestMethod]
        public void TestMethod1()
        {
            var class1 = new Class1();

            var commitsLog_a = class1.GetListOfCommitsHistory(ThisRepo);
            var commitsLog_b = class1.GetListOfCommitsHistory(DemoRepo);

            var uncommiteChanges_a = class1.GetListOfPendingChanges(ThisRepo);
            var uncommiteChanges_b = class1.GetListOfPendingChanges(DemoRepo);

            var isRepositoryHavePendingChanges_a = class1.IsRepositoryHavePendingChanges(ThisRepo);
            var isRepositoryHavePendingChanges_b = class1.IsRepositoryHavePendingChanges(DemoRepo);

            var diff_a = class1.GetDiff(ThisRepo);
            var diff_b = class1.GetDiff(DemoRepo);


        }
    }
}

