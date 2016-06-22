using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFCResourceGeneratorTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void GenerateFile()
        {
            MFCResourceGenerator.Program.Main(new string[] {
                "--p", "this",
                "--o", "out" });
        }
    }
}
