using GatesBallGameApplication;
using System;

namespace GatesBallGameApplication
{
    static class Program
    {
        #region Main method

        /// <summary>
        /// Main method of the program
        /// </summary>
        /// <param name="args">none at this minute</param>
        static void Main(string[] args)
        {
            Console.WriteLine("*****Ready for Ball Game********");

           
           
                Console.WriteLine("\n  4 Levels  15 Balls and 16 Containers --  ");
                string inputValue = "4";

                string LevelErrorMessage = InputValidator.LevelEval(inputValue, out int Level);
                if (string.IsNullOrEmpty(LevelErrorMessage))
                {
                    long totalContainers = (long)Math.Pow(2, Level);
                    
                    string AssumptionValue = "15";//Console.ReadLine();

                    string AssumptionErrorMessage = InputValidator.AssumptionEmptyContainerValidator(AssumptionValue, totalContainers, out long AssumptionEmptyContainer);
                    if (string.IsNullOrEmpty(AssumptionErrorMessage))
                    {
                        BallGateDTO.RunBallGateModel(Level, totalContainers, AssumptionEmptyContainer);
                    }
                    else
                    {
                        Console.WriteLine(AssumptionErrorMessage);
                    }
                }
                else
                {
                    Console.WriteLine(LevelErrorMessage);
                }

            Console.Read();
        }

        #endregion
    }
}
