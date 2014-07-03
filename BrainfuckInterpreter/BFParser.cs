namespace BrainfuckInterpreter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Parses Brain-fuck code and executes the operations
    /// </summary>
    public class BFParser
    {
        /// <summary>
        /// Stores the instructions to execute
        /// </summary>
        private string program;

        /// <summary>
        /// A pointer to the current instruction
        /// </summary>
        private int instructionPointer;

        /// <summary>
        /// The simulated machine upon which the parsed code is executed
        /// </summary>
        private DataStorage storageMachine;

        /// <summary>
        /// Initializes a new instance of the BFParser class
        /// </summary>
        /// <param name="program">the Brain-fuck instructions</param>
        public BFParser(string program)
        {
            if(string.IsNullOrWhiteSpace(program)) throw new ArgumentNullException(program);

            this.program = program;
            this.Initialise();
        }

        /// <summary>
        /// Gets the program instructions
        /// </summary>
        public string Program { get { return this.program; } }

        /// <summary>
        /// Gets a pointer to the current instruction
        /// </summary>
        public int InstructionPointer { get { return this.instructionPointer; } }

        /// <summary>
        /// Gets the parser's storage machine
        /// </summary>
        public DataStorage StorageMachine { get { return this.storageMachine; } }

        /// <summary>
        /// Performs initialization logic necessary to correctly parse the instructions
        /// </summary>
        private void Initialise()
        {
            this.instructionPointer = 0;
            this.storageMachine = new DataStorage();
        }

        /// <summary>
        /// Runs the program
        /// </summary>
        /// <param name="isDebugMode">Should the program write debug info to the console</param>
        public void Run(bool isDebugMode)
        {
            // Initialise the parser
            this.Initialise();

            // A variable to keep track of how many instructions have been processed
            // Only used with debugMode
            int instructionsProcessed = 0;

            try
            {

                // Process instructions until the end of the program is reached
                while (instructionPointer < this.program.Length)
                {
                    // Get the instruction
                    char currentInstruction = program[this.instructionPointer];

                    // Display debug information
                    if (isDebugMode)
                    {
                        // Display current instruction
                        Console.WriteLine("Instruction {0}: {1}", instructionsProcessed++.ToString(), currentInstruction);

                        // Display the value at the instruction pointer
                        Console.WriteLine("Value at {0} is {1}", storageMachine.PointerLocation, storageMachine.CurrentValue);

                        // Display location within program
                        Console.WriteLine(program);
                        for (int i = 0; i < instructionPointer; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.WriteLine("^");

                        // Display a linebreak
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine();
                    }

                    // Process the instruction
                    switch (currentInstruction)
                    {
                        case '>':
                            // Move pointer right
                            this.storageMachine.IncrementPointer();
                            break;

                        case '<':
                            // Move pointer left
                            this.storageMachine.DecrementPointer();
                            break;

                        case '+':
                            // Increment the value at the pointer
                            this.storageMachine.IncrementByte();
                            break;

                        case '-':
                            // Decrement the value at the pointer
                            this.storageMachine.DecrementByte();
                            break;

                        case '.':
                            // Output the value
                            this.Output(isDebugMode);
                            break;

                        case ',':
                            // Read a value
                            this.Input();
                            break;

                        case '[':
                            // If the current value is 0 then jump to the end of the code block, otherwise proceed into it
                            if (storageMachine.CurrentValue == 0)
                            {
                                instructionPointer = GetClosingBracketIndex();
                            }
                            break;

                        case ']':
                            // If the current value is non-0 then jump to the beginning of the codeblock, otherwise proceed onwards
                            if (storageMachine.CurrentValue != 0)
                            {
                                instructionPointer = GetOpeningBracketIndex();
                            }
                            break;

                        default:
                            // Do nothing in the case of an unknown character
                            break;
                    }

                    // Move on to the next instruction
                    instructionPointer++;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to continue execution! Invalid code");
                Console.WriteLine("Check your brackets");
            }
        }

        /// <summary>
        /// Finds the closing bracket that matches the opening bracket currently at the instruction pointer
        /// </summary>
        /// <returns>The index of the closing bracket</returns>
        private int GetClosingBracketIndex()
        {
            // Used to keep track of the depth of nesting of [] brackets
            int depth = 0;

            // loop through the program starting from the next char
            for (int i = instructionPointer + 1; i < program.Length; i++)
            {
                char currentChar = program[i];
                switch (currentChar)
                {
                    case '[' :
                        // If we encounter an opening bracket the nesting depth is increasing
                        depth++;
                        break;

                    case ']' :
                        if (depth == 0)
                        {
                            // If we encounter a closing bracket and there is no nesting depth this is the relevant closing bracket
                            return i;
                        }
                        else
                        {
                            // If there was a nesting depth then make it 1 shallower
                            depth--;
                        }
                        break;

                    default :
                        // Do nothing
                        break;
                }
            }

            // If no matching bracket was found the code must be invalid
            throw new FormatException("Unmatched bracket");
        }

        /// <summary>
        /// Finds the opening bracket that matches the closing bracket currently at the instruction pointer
        /// </summary>
        /// <returns>The index of the closing bracket</returns>
        private int GetOpeningBracketIndex()
        {
            // Used to keep track of the depth of nesting of [] brackets
            int depth = 0;

            // loop through the program starting from the next char
            for (int i = instructionPointer - 1; i >= 0; i--)
            {
                char currentChar = program[i];
                switch (currentChar)
                {
                    case '[':
                        if (depth == 0)
                        {
                            // If we encounter an opening bracket and there is no nesting depth this is the relevant opening bracket
                            return i;
                        }
                        else
                        {
                            // If there was a nesting depth then make it 1 shallower
                            depth--;
                        }
                        break;

                    case ']':
                        // If we encounter a closing bracket the nesting depth is increasing
                        depth++;
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            // If no matching bracket was found the code must be invalid
            throw new FormatException("Unmatched bracket");

        }
        
        /// <summary>
        /// Outputs the storage machine's current value to the console
        /// </summary>
        /// <param name="isDebugMode">Should the program write debug info to the console</param>
        private void Output(bool isDebugMode)
        {
            // Get the current byte
            byte output = this.storageMachine.CurrentValue;

            // Convert the value to UFT8
            byte[] outputArray = new byte[1] { output };
            string outputUFT8 = System.Text.Encoding.UTF8.GetString(outputArray);

            if (!isDebugMode)
            {
                // Write the character to console
                Console.Write(outputUFT8);
            }
            else
            {
                // Format the output and write it to console
                string outputString = string.Format("{0}, {1}", output.ToString().PadLeft(3, '0'), outputUFT8);
                Console.WriteLine(outputString);
            }
        }

        /// <summary>
        /// Reads input from the console
        /// </summary>
        private void Input()
        {
            string input = "";
            while (!isValidByte(input))
            {
                Console.WriteLine("Input a valid byte:");
                input = Console.In.ReadToEnd();
            }
        }

        /// <summary>
        /// Checks whether a given string contains a valid byte
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool isValidByte(string input)
        {
            int number;

            try
            {
                number = int.Parse(input);
            }
            catch (ArgumentNullException)
            {
                // input was null
                return false;
            }
            catch (FormatException)
            {
                // input was incorrectly formatted
                return false;
            }
            catch (OverflowException)
            {
                // input was out of range
                return false;
            }

            if (number < byte.MinValue || number > byte.MaxValue)
            {
                // out of valid byte range
                return false;
            }

            return true;
        }
    }
}
