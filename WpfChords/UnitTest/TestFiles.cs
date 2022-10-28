using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using WpfChords;

namespace UnitTest
{
    [TestClass]
    public class TestFiles
    {
        [TestMethod]
        public void TestMethod1()
        {
            ArrayList arrayList = new ArrayList();
            ArrayList expected = new ArrayList();

            string[] ch = new string[20];
            ch[0] = "Am";
            expected.Add("C:/Users/Machenike/source/repos/WpfChords/MusicFiles/Chords Am.wav");

            MainWindow c = new MainWindow();
            ArrayList actual = c.ConvertToFile(ch);
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            string testtext = "aaa";
            bool expected = false;
            MainWindow c = new MainWindow();
            bool actual = c.IsNumber(testtext);

            Assert.AreEqual(expected, actual);

        }
    }
}
