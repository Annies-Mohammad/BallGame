using System.Globalization;

namespace GatesBallGameApplication
{
    public static class InputValidator
    {
        #region Public static methods

        /// <summary>
        /// Validates the input provided for Level of the model
        /// </summary>
        /// <param name="inputValue">input value for Level</param>
        /// <param name="Level">Level -- an out argument</param>
        /// <returns>Error message if any; else string.empty</returns>
        public static string LevelEval(string inputValue, out int Level)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                Level = 0;
                return "Invalid input for Level--Please correct";
            }

            if (int.TryParse(inputValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out Level))
            {
                if (Level < 2)
                {
                    return "Level should always be more than 1 --";
                }
            }
            else
            {
                return "Invalid input for Level!";
            }

            return string.Empty;
        }

        /// <summary>
        /// Validates whether input for Assumption empty container is valid or not
        /// </summary>
        /// <param name="AssumptionValue">Assumption empty container value</param>
        /// <param name="totalContainers">total containers basedon Level</param>
        /// <param name="AssumptionEmptyContainer">Assumption empty container value if input is valid</param>
        /// <returns>Error message if any; else string.empty</returns>
        public static string AssumptionEmptyContainerValidator(string AssumptionValue, long totalContainers, out long AssumptionEmptyContainer)
        {
            if (long.TryParse(AssumptionValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out AssumptionEmptyContainer))
            {
                if (AssumptionEmptyContainer >= 1 && AssumptionEmptyContainer <= totalContainers)
                {
                    return string.Empty;
                }

                return string.Format("\n Assumption should be in the range [1, {0}]", totalContainers);
            }

            return "Invalid Assumption!";
        }

        #endregion
    }
}
