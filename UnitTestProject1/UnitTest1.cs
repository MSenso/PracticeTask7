using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTask7;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Form1 form1 = new Form1();
            string vector = "01011011";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "0";
            if (form1.Check_Input(vector))
            form1.Check_Classes(vector);

            vector = "1";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "1010";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "00010011";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "10111110";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "0000000000000000";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "00000000000000000";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "000";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);

            vector = "0a1v";
            if (form1.Check_Input(vector))
                form1.Check_Classes(vector);
        }
    }
}
