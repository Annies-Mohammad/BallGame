using GatesBallGameApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BallGate
{
    [TestClass]
    public class InputValidatorUnitTest
    {
        [TestMethod]
        public void LevelEval_NULL_Input_Test()
        {
            string error = InputValidator.LevelEval("", out int Level);

            Assert.AreEqual("Invalid input for Level!", error);
            Assert.AreEqual(0, Level);
        }

        [TestMethod]
        public void LevelEval_Blank_Input_Test()
        {
            string error = InputValidator.LevelEval(" ", out int Level);

            Assert.AreEqual("Invalid input for Level!", error);
            Assert.AreEqual(0, Level);
        }

        [TestMethod]
        public void LevelEval_String_Input_Test()
        {
            string error = InputValidator.LevelEval("aaa", out int Level);

            Assert.AreEqual("Invalid input for Level!", error);
            Assert.AreEqual(0, Level);
        }

        [TestMethod]
        public void LevelEval_Negative_Input_Test()
        {
            string error = InputValidator.LevelEval("-10", out int Level);

            Assert.AreEqual("Level should always be more than 1!", error);
            Assert.AreEqual(-10, Level);
        }

        [TestMethod]
        public void LevelEval_One_Input_Test()
        {
            string error = InputValidator.LevelEval("1", out int Level);

            Assert.AreEqual("Level should always be more than 1!", error);
            Assert.AreEqual(1, Level);
        }

        [TestMethod]
        public void LevelEval_Valid_Input_Test()
        {
            string error = InputValidator.LevelEval("10", out int Level);

            Assert.AreEqual("", error);
            Assert.AreEqual(10, Level);
        }

        [TestMethod]
        public void AssumptionValidator_NULL_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator("", 10, out long emptyContainerAssumption);

            Assert.AreEqual("Invalid Assumption!", error);
            Assert.AreEqual(0, emptyContainerAssumption);
        }

        [TestMethod]
        public void AssumptionValidator_Blank_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator(" ", 10, out long emptyContainerAssumption);

            Assert.AreEqual("Invalid Assumption!", error);
            Assert.AreEqual(0, emptyContainerAssumption);
        }

        [TestMethod]
        public void AssumptionValidator_String_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator("aaa", 10, out long emptyContainerAssumption);

            Assert.AreEqual("Invalid Assumption!", error);
            Assert.AreEqual(0, emptyContainerAssumption);
        }

        [TestMethod]
        public void AssumptionValidator_Negative_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator("-100", 10, out long emptyContainerAssumption);

            Assert.AreEqual("\n Assumption should be in the range [1, 10]", error);
            Assert.AreEqual(-100, emptyContainerAssumption);
        }

        [TestMethod]
        public void AssumptionValidator_LessThan_Lower_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator("-1", 10, out long emptyContainerAssumption);

            Assert.AreEqual("\n Assumption should be in the range [1, 10]", error);
            Assert.AreEqual(-1, emptyContainerAssumption);
        }

        [TestMethod]
        public void AssumptionValidator_GreaterThan_Upper_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator("12", 10, out long emptyContainerAssumption);

            Assert.AreEqual("\n Assumption should be in the range [1, 10]", error);
            Assert.AreEqual(12, emptyContainerAssumption);
        }

        [TestMethod]
        public void AssumptionValidator_Valid_Input_Test()
        {
            string error = InputValidator.AssumptionEmptyContainerValidator("2", 10, out long emptyContainerAssumption);

            Assert.AreEqual(string.Empty, error);
            Assert.AreEqual(2, emptyContainerAssumption);
        }
    }
}
