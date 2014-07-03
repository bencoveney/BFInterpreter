namespace BrainfuckInterpreter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Launches execution of the Brain-fuck interpreter
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the Brain-fuck interpreter's execution
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            Console.SetWindowSize(160, 60);
            BFParser parser = new BFParser("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.");
            parser.Run(false);
            Console.Read();
        }
    }
}
