namespace BrainfuckInterpreter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Simulates a simple byte storage machine
    /// </summary>
    public class DataStorage
    {
        /// <summary>
        /// The data storage
        /// </summary>
        private byte[] dataCells;

        /// <summary>
        /// The current location within the data storage
        /// </summary>
        private int dataPointer;

        /// <summary>
        /// Initializes a new instance of the DataStorage class
        /// </summary>
        public DataStorage()
        {
            // Create the data cells and initialise them to 0
            // Assume 30000 bytes
            this.dataCells = new byte[30000];
            for (int i = 0; i < this.dataCells.Count(); i++)
            {
                this.dataCells[i] = 0;
            }

            // Initialise the data pointer
            this.dataPointer = 0;
        }

        /// <summary>
        /// Gets the value of the currently selected byte
        /// </summary>
        public byte CurrentValue
        {
            get
            {
                return this.dataCells[this.dataPointer];
            }
        }

        /// <summary>
        /// Gets the location of the data pointer
        /// </summary>
        public int PointerLocation
        {
            get
            {
                return this.dataPointer;
            }
        }

        /// <summary>
        /// Gets the value at a specified location
        /// </summary>
        /// <param name="index">The memory location to read from</param>
        /// <returns>The value from memory</returns>
        public byte this[int index]
        {
            get
            {
                return this.dataCells[index];
            }
        }

        /// <summary>
        /// Moves the data pointer to the cell to the right
        /// </summary>
        public void IncrementPointer()
        {
            if (this.dataPointer + 1 < this.dataCells.Count())
            {
                // If we havent reached the right limit increment the pointer
                this.dataPointer++;
            }
            else
            {
                // If we have reached the right limit reset the pointers position
                this.dataPointer = 0;
            }
        }

        /// <summary>
        /// Moves the data pointer to the cell to the left
        /// </summary>
        public void DecrementPointer()
        {
            if (this.dataPointer - 1 >= 0)
            {
                // If we havent reached the left limit decrement the pointer
                this.dataPointer--;
            }
            else
            {
                // if we have reached the left limit move to the end of the byte list
                this.dataPointer = this.dataCells.Count();
            }
        }

        /// <summary>
        /// Increases the value of the byte at the data pointer by 1
        /// </summary>
        public void IncrementByte()
        {
            if (this.dataCells[this.dataPointer] + 1 <= byte.MaxValue)
            {
                // If we can increase the value then do so
                this.dataCells[this.dataPointer]++;
            }
            else
            {
                // If we are over the max value revert to 0
                this.dataCells[this.dataPointer] = 0;
            }
        }

        /// <summary>
        /// Decreases the value of the byte at the 
        /// </summary>
        public void DecrementByte()
        {
            if (this.dataCells[this.dataPointer] - 1 >= byte.MinValue)
            {
                // If we can decrease the value then do so
                this.dataCells[this.dataPointer]--;
            }
            else
            {
                // If we are under the min value revert to the max value
                this.dataCells[this.dataPointer] = byte.MaxValue;
            }
        }

        /// <summary>
        /// Sets the value at the current location
        /// </summary>
        /// <param name="input">The value to assign</param>
        public void SetByte(byte input)
        {
            this.dataCells[this.dataPointer] = input;
        }
    }
}
