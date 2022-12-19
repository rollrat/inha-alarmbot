using alarmbot.Res;
using alarmbot.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace alarmbot.Test
{
    [TestClass]
    public class ParsingTestCodeGenerator
    {
        [TestMethod]
        public void generate()
        {
            foreach(var dept in DepartmentList.Lists)
            {
                if (dept.Item2.Length == 2)
                    Console.WriteLine($"[TestMethod] public void {dept.Item1}ParsingTest() => Test{dept.Item2[1]}(\"{dept.Item3}\");");
            }
        }
    }
}
