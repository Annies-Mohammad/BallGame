# BallGame
This dynamic model describes a system of branches and balls. At each intersection is a gate. When a ball passes a gate it will pass down the open side, but then the gate will move to the other side. There are 15 balls and 16 containers. Which container will not receive a ball when all balls have passed through  the system.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {

        static void Main(string[] args)
        {
            Tree t = new Tree(4);
            t.RunModel(15);
        }

        // Define other methods and classes here
        internal static Random _booleanRandom = new Random();
        public static bool GetRandomBoolean()
        {
            return _booleanRandom.NextDouble() > 0.5;
        }

        public enum GateSide { LEFT, RIGHT }

        public class Node
        {
            public Node LeftBranch;
            public Node RightBranch;

            // Refactored to "GateSide" enum for readability
            public GateSide SideOpen;
            // Only need to know how many balls have passed through the lowest child node
            public int BallsPassed;
            public Node()
            {
                // Rules say that node should have left/right gate open/close decided randomly
                SideOpen = GetRandomBoolean() ? GateSide.LEFT : GateSide.RIGHT;
            }
            public void InvertGate()
            {
                SideOpen = SideOpen == GateSide.LEFT ? GateSide.RIGHT : GateSide.LEFT;
            }
            // Recursively passes a ball down child nodes
            public void PassBall()
            {
                // Left & Right branches are null for nodes at the bottom of the tree
                if (LeftBranch == null && RightBranch == null)
                {
                    BallsPassed++;
                    return;
                }
                else
                {
                    if (SideOpen == GateSide.LEFT)
                        LeftBranch.PassBall();
                    else
                        RightBranch.PassBall();
                }
                // Gate is inverted every time a ball passes
                InvertGate();
            }
            // Recursively creates child nodes
            public void CreateChildren(int number)
            {
                if (number == 0)
                    return;
                LeftBranch = new Node();
                LeftBranch.CreateChildren(number - 1);
                RightBranch = new Node();
                RightBranch.CreateChildren(number - 1);
            }

        }

        public class Tree
        {
            public Node Root;

            // System needs to be instatiated with "Depth", That is, the amount of paths
            public Tree(int Depth)
            {
                Root = new Node();
                Root.CreateChildren(Depth);
            }
            public void PredictOutcome()
            {
                // need to implement an algorithm that examines the tree and algorithmically determines the outcome
                // i.e. which branch will have 0 balls go down it
            }
            // Runs the model with the specified ball count
            public void RunModel(int count)
            {
                while (count > 0)
                {
                    Root.PassBall();
                    count--;
                }
            }
        }


       
     
    }
}
