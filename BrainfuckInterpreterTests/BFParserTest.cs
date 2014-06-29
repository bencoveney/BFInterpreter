using BrainfuckInterpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BrainfuckInterpreterTests
{
    
    
    /// <summary>
    ///This is a test class for BFParserTest and is intended
    ///to contain all BFParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BFParserTest
    {
        #region Constructor Tests

        /// <summary>
        /// Checks the construction and that data members are assigned
        /// </summary>
        [TestMethod()]
        public void BFParserConstructorTest()
        {
            BFParser target = new BFParser("+.");

            // Checks the data members were correctly initialised
            Assert.AreEqual(target.Program, "+.");
            Assert.AreEqual(target.InstructionPointer, 0);
            Assert.AreEqual(target.StorageMachine.ToString(), new DataStorage().ToString());
        }

        /// <summary>
        /// Checks a parser cannot be constructed with a null string
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BFParserConstructorNullProgramTest()
        {
            string nullString = null;

            BFParser target = new BFParser(nullString);
        }

        /// <summary>
        /// Checks a parser cannot be constructed with a empty string
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BFParserConstructorEmptyProgramTest()
        {
            string emptyString = " ";

            BFParser target = new BFParser(emptyString);
        }

        #endregion

        #region isValidByte Tests

        /// <summary>
        /// Tests that IsValidByte accepts valid input
        ///</summary>
        [TestMethod()]
        public void isValidByteTest()
        {
            // Create a new Parser
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Test the value
            bool isZeroValid = target.isValidByte("0");

            // Check the result
            Assert.AreEqual(isZeroValid, true);
        }

        /// <summary>
        /// Tests that IsValidByte accepts valid input
        ///</summary>
        [TestMethod()]
        public void isValidByteNullTest()
        {
            // Create a new Parser
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Test the value
            bool isZeroValid = target.isValidByte(null);

            // Check the result
            Assert.AreEqual(false, isZeroValid);
        }

        /// <summary>
        /// Tests that IsValidByte accepts valid input
        ///</summary>
        [TestMethod()]
        public void isValidByteTooLowTest()
        {
            // Create a new Parser
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Test the value
            bool isNegativeOne = target.isValidByte("-1");

            // Check the result
            Assert.AreEqual(false, isNegativeOne);
        }

        /// <summary>
        /// Tests that IsValidByte accepts valid input
        ///</summary>
        [TestMethod()]
        public void isValidByteTooHighTest()
        {
            // Create a new Parser
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Test the value
            bool is256Valid = target.isValidByte("256");

            // Check the result
            Assert.AreEqual(false, is256Valid);
        }

        /// <summary>
        /// Tests that IsValidByte accepts valid input
        ///</summary>
        [TestMethod()]
        public void isValidByteNaNTest()
        {
            // Create a new Parser
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Test the value
            bool isNaNValid = target.isValidByte("Not A Number!");

            // Check the result
            Assert.AreEqual(false, isNaNValid);
        }

        #endregion

        #region Run Tests

        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest()
        {
            // A program which defines a basic "Hello World" program
            // Taken from http://en.wikipedia.org/wiki/Brainfuck#Hello_World.21
            string helloWorld = "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.";

            // Create a parser
            BFParser target = new BFParser(helloWorld);

            // Initialise parameters
            bool isDebugMode = false;

            using (StringWriter sw = new StringWriter())
            {
                // Set the console to output to a readable source
                Console.SetOut(sw);

                // run the program
                target.Run(isDebugMode);

                // Check the output
                string expected = "Hello World!" + (char)10;
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        #endregion

        #region Output Tests

        /// <summary>
        /// Tests that the method triggers a write to console
        ///</summary>
        [TestMethod()]
        public void OutputTest()
        {
            // Create a basic program
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Initialise parameters
            bool isDebugMode = false;

            using (StringWriter sw = new StringWriter())
            {
                // Set the console to output to a readable source
                Console.SetOut(sw);

                // run the program
                target.Output(isDebugMode);

                // Expect a control caracter (UFT8 char 0)
                string expected = "\0";

                // Check the output
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        /// <summary>
        /// Tests that the method gives correct output under debug mode
        ///</summary>
        [TestMethod()]
        public void OutputDebugModeTest()
        {
            // Create a basic program
            BFParser_Accessor target = new BFParser_Accessor("+.");

            // Initialise parameters
            bool isDebugMode = true;

            using (StringWriter sw = new StringWriter())
            {
                // Set the console to output to a readable source
                Console.SetOut(sw);

                // run the program
                target.Output(isDebugMode);

                // Expect a control caracter (UFT8 char 0)
                string expected = string.Format("000, \0{0}", Environment.NewLine);

                // Check the output
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        /// <summary>
        /// Tests that the method outputs the correct character after a program has been run
        ///</summary>
        [TestMethod()]
        public void OutputOtherCharTest()
        {
            // Create a basic program
            // 33 pluses to get to an exclamation mark (UFT char 34)
            BFParser_Accessor target = new BFParser_Accessor("+++++++++++++++++++++++++++++++++.");

            // Initialise parameters
            bool isDebugMode = false;

            // Run the program
            target.Run(isDebugMode);

            using (StringWriter sw = new StringWriter())
            {
                // Set the console to output to a readable source
                Console.SetOut(sw);

                // run the program
                target.Output(isDebugMode);

                // Check the output
                Assert.AreEqual("!", sw.ToString());
            }
        }

        #endregion

        #region Input Tests

        ///// <summary>
        /////A test for Input
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("BrainfuckInterpreter.exe")]
        //public void InputTest()
        //{
        //    // Create a basic program
        //    BFParser_Accessor target = new BFParser_Accessor("+.");

        //    // Create an input string
        //    string input = string.Format("!{0}Next Line", Environment.NewLine);

        //    using (StringReader sr = new StringReader(input))
        //    {
        //        // Set the console to output to a readable source
        //        Console.SetIn(sr);

        //        // run the program
        //        target.Input();

        //        // Expect a control caracter (UFT8 char 0)
        //        string expected = "34";

        //        // Check the output
        //        Assert.AreEqual(expected, target.StorageMachine.CurrentValue);
        //    }
        //}

        #endregion

        #region Initialise Tests

        /// <summary>
        /// Tests that initialise resets data members
        /// </summary>
        [TestMethod()]
        public void InitialiseTest()
        {
            // create a program which mutates the data storage and instruction pointer
            string program = "+++>+++>+++++++>++++.";

            // Create a parser
            BFParser_Accessor target = new BFParser_Accessor(program);

            // Run the program
            target.Run(false);

            // Re-Initialise the parser
            target.Initialise();

            // Checks the data members were correctly re-initialised
            Assert.AreEqual(target.InstructionPointer, 0);
            Assert.AreEqual(target.StorageMachine.ToString(), new DataStorage().ToString());
        }

        #endregion

        #region GetClosingBracketIndex Tests

        /// <summary>
        ///A test for GetClosingBracketIndex
        ///</summary>
        [TestMethod()]
        public void GetClosingBracketIndexTest()
        {
            // create a parser using a test program
            string bracketTestProgram = "+[+[-]-]+";
            BFParser_Accessor target = new BFParser_Accessor(bracketTestProgram);

            // move the instruction pointer to the first bracket
            target.instructionPointer = 1;

            // find the closing bracket
            int actual = target.GetClosingBracketIndex();

            // check the result
            int expected = 7;
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetOpeningBracketIndex Tests

        /// <summary>
        ///A test for GetOpeningBracketIndex
        ///</summary>
        [TestMethod()]
        public void GetOpeningBracketIndexTest()
        {
            // create a parser using a test program
            string bracketTestProgram = "+[+[-]-]+";
            BFParser_Accessor target = new BFParser_Accessor(bracketTestProgram);

            // move the instruction pointer to the first bracket
            target.instructionPointer = 7;

            // find the closing bracket
            int actual = target.GetOpeningBracketIndex();

            // check the result
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
