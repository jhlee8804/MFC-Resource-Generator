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
            string workingPath = "..\\..\\";
            string resourcePath = workingPath + "Resource";

            MFCResourceGenerator.Program.Main(new string[] {
                "--wpath", workingPath,
                "--rpath", resourcePath,
                "--v", true.ToString() 
            });
        }
    }
}
