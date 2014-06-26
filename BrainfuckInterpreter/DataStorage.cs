using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrainfuckInterpreter
{
    class DataStorage
    {
        private byte[] dataCells;
        private int dataPointer;

        public byte CurrentValue { get { return dataCells[dataPointer] } }

        public DataStorage()
        {
            // Create the data cells and initialise them to 0
            // Assume 30000 bytes
            dataCells = new byte[30000];
            for(int i = 0; i < dataCells.Count(); i++)
            {
                dataCells[i] = 0;
            }

            // Initialise the data pointer
            dataPointer = 0;
        }

        /// <summary>
        /// Moves the data pointer to the cell to the right
        /// </summary>
        public void IncrementPointer()
        {
            if (dataPointer + 1 < dataCells.Count())
            {
                // If we havent reached the right limit increment the pointer
                dataPointer++;
            }
            else
            {
                // If we have reached the right limit reset the pointers position
                dataPointer = 0;
            }
        }

        /// <summary>
        /// Moves the data pointer to the cell to the left
        /// </summary>
        public void DecrementPointer()
        {
            if (dataPointer - 1 > 0)
            {
                // If we havent reached the left limit decrement the pointer
                dataPointer--;
            }
            else
            {
                // if we have reached the left limit move to the end of the byte list
                dataPointer = dataCells.Count();
            }
        }

        /// <summary>
        /// Increases the value of the byte at the data pointer by 1
        /// </summary>
        public void IncrementByte()
        {
            if (CurrentValue + 1 <= byte.MaxValue)
            {
                // If we can increase the value then do so
                dataCells[dataPointer]++;
            }
            else
            {
                // If we are over the max value revert to 0
                dataCells[dataPointer] = 0;
            }
        }

        /// <summary>
        /// Decreases the value of the byte at the 
        /// </summary>
        public void DecrementByte() {
            if (CurrentValue - 1 >= byte.MinValue)
            {
                // If we can decrease the value then do so
                dataCells[dataPointer]--;
            }
            else
            {
                // If we are under the min value revert to the max value
                dataCells[dataPointer] = byte.MaxValue;
            }
        }

        public void SetByte(byte Input)
        {
            dataCells[dataPointer] = Input;
        }
    }
}
