using BrainfuckInterpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BrainfuckInterpreterTests
{
    
    
    /// <summary>
    ///This is a test class for DataStorageTest and is intended
    ///to contain all DataStorageTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataStorageTest
    {
        #region Constructor Tests

        /// <summary>
        ///A test for DataStorage Constructor
        ///Checks that the dataStorage data member was created and assigned
        ///</summary>
        [TestMethod()]
        public void DataStorageConstructorDataCellsTest()
        {
            DataStorage_Accessor target = new DataStorage_Accessor();

            Assert.AreEqual(30000, target.dataCells.Length);
        }

        /// <summary>
        ///A test for DataStorage Constructor
        ///Checks that the dataPointer data member was created and assigned
        ///</summary>
        [TestMethod()]
        public void DataStorageConstructorDataPointerTest()
        {
            DataStorage_Accessor target = new DataStorage_Accessor();

            Assert.AreEqual(0, target.dataPointer);
        }

        #endregion

        /// <summary>
        ///A test for DecrementByte
        ///</summary>
        [TestMethod()]
        public void DecrementByteTest()
        {
            // Arrange
            // Set a data cell to 99 and point at that cell
            DataStorage_Accessor datastorage = new DataStorage_Accessor();
            datastorage.dataCells[5] = 99;
            datastorage.dataPointer = 5;

            // Act
            // Decrement that cell's value
            datastorage.DecrementByte();

            // Assert
            byte expected = 98;
            Assert.AreEqual(expected, datastorage.CurrentValue);
        }

        /// <summary>
        ///A test for DecrementPointer
        ///</summary>
        [TestMethod()]
        public void DecrementPointerTest()
        {
            // Arrange
            // Set the data pointer's location
            DataStorage_Accessor datastorage = new DataStorage_Accessor();
            datastorage.dataPointer = 5;

            // Act
            // Decrement the pointer's value
            datastorage.DecrementPointer();

            // Assert
            int expected = 4;
            Assert.AreEqual(expected, datastorage.PointerLocation);
        }

        /// <summary>
        ///A test for IncrementByte
        ///</summary>
        [TestMethod()]
        public void IncrementByteTest()
        {
            // Arrange
            // Set a data cell to 99 and point at that cell
            DataStorage_Accessor datastorage = new DataStorage_Accessor();
            datastorage.dataCells[5] = 99;
            datastorage.dataPointer = 5;

            // Act
            // Increment that cell's value
            datastorage.IncrementByte();

            // Assert
            byte expected = 100;
            Assert.AreEqual(expected, datastorage.CurrentValue);
        }

        /// <summary>
        ///A test for IncrementPointer
        ///</summary>
        [TestMethod()]
        public void IncrementPointerTest()
        {
            // Arrange
            // Set the data pointer's location
            DataStorage_Accessor datastorage = new DataStorage_Accessor();
            datastorage.dataPointer = 5;

            // Act
            // Increment the pointer's value
            datastorage.IncrementPointer();

            // Assert
            int expected = 6;
            Assert.AreEqual(expected, datastorage.PointerLocation);
        }

        /// <summary>
        ///A test for SetByte
        ///</summary>
        [TestMethod()]
        public void SetByteTest()
        {
            // Arrange
            // Set a data cell to 99 and point at that cell
            DataStorage_Accessor datastorage = new DataStorage_Accessor();
            datastorage.dataCells[5] = 99;
            datastorage.dataPointer = 5;

            // Act
            // Set that cell's value
            datastorage.SetByte(69);

            // Assert
            byte expected = 69;
            Assert.AreEqual(expected, datastorage.CurrentValue);
        }
    }
}
