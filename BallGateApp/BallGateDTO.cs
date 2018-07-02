using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GatesBallGameApplication
{
    public static class BallGateDTO
    {
        #region Private fiedls

        /// <summary>
        /// Int maintained to provide the HeadId for the last level children nodes
        /// </summary>
        static long _globalHeadId;

        #endregion

        #region Public static methods

        /// <summary>
        /// Runs the complete ball gate model
        /// 1. Creates the ball gate model initially -- handles OutOfMemory Exception
        /// 2. Passes the balls throught the model
        /// 3. Checks for the empty container 
        /// </summary>
        /// <param name="Level">Level of the ball gate model</param>
        /// <param name="totalContainers">total number of nodes in the ball gate model</param>
        /// <param name="AssumptionEmptyContainer">empty container id Assumptioned by the user</param>
        public static void RunBallGateModel(int Level, long totalContainers, long AssumptionEmptyContainer)
        {
            _globalHeadId = 0;

            var tokenSource = new CancellationTokenSource();

            try
            {
                Console.WriteLine("Starting..................");
                using (NodeInfo rootNode = new NodeInfo(NodeInfo.GetGateState()))
                {
                    Traverse(0, Level, rootNode, tokenSource, tokenSource.Token);                    

                    Console.WriteLine("\n Balls rolling started");
                    PassBalls(totalContainers - 1, rootNode);
                    Console.WriteLine("\n Balls Rolling finished.");

                    Console.WriteLine("\n Just a moment to know which container is empty");
                    CheckEmptyContainer(rootNode, AssumptionEmptyContainer, tokenSource, tokenSource.Token);
                    
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("System cannot handle Level of {0}; please try with lesser Level. \n Error: {1}", Level, ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("System cannot handle Level of {0}; please try with lesser Level. \n Error: {1}", Level, e.Message);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Checking for the empty container after all the balls are passed
        /// </summary>
        /// <param name="rootNode">Root node of the ball gate problem</param>
        /// <param name="AssumptionEmptyContainer">Head Id Assumptioned by the user</param>
        /// <param name="tokenSource">TokenSource object for creating tasks</param>
        /// <param name="token">Token of the cancellation token source</param>
        public static void CheckEmptyContainer(NodeInfo rootNode, long AssumptionEmptyContainer, CancellationTokenSource tokenSource, CancellationToken token)
        {
            if (rootNode == null)
            {
                return;
            }

            if (rootNode.BallCount == 0 && rootNode.RightChild == null && rootNode.LeftChild == null)
            {
                Console.WriteLine(" Empty container isssssssssssssss ******: {0}", rootNode.NodeId);
                if (AssumptionEmptyContainer == rootNode.NodeId)
                {
                    Console.WriteLine("Your Assumption number is correct.");
                }
                tokenSource.Cancel();
                return;
            }

            try
            {
                if (!token.IsCancellationRequested)
                {
                    var t1 = Task.Factory.StartNew(() => CheckEmptyContainer(rootNode.LeftChild, AssumptionEmptyContainer, tokenSource, token), token);
                    var t2 = Task.Factory.StartNew(() => CheckEmptyContainer(rootNode.RightChild, AssumptionEmptyContainer, tokenSource, token), token);
                    if (!t1.IsCanceled && !t2.IsCanceled)
                    {
                        Task.WaitAll(t1, t2);
                    }
                }
            }
            catch (Exception)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
            }          
        }

        /// <summary>
        /// Passing the balls
        /// Running sequentially as the tree state should be maintained 
        /// </summary>
        /// <param name="noOfBalls">Number of balls</param>
        /// <param name="startPoint">Start point</param>
        public static void PassBalls(long noOfBalls, NodeInfo startPoint)
        {
            for (long ithBall = 1; ithBall <= noOfBalls; ithBall++)
            {
                NodeInfo ballPosition = startPoint;

                do
                {
                    if (ballPosition.GateState == GateState.Left)
                    {
                        ballPosition.GateState = GateState.Right;
                        ballPosition = ballPosition.LeftChild;
                    }
                    else
                    {
                        ballPosition.GateState = GateState.Left;
                        ballPosition = ballPosition.RightChild;
                    }
                } while (ballPosition.LeftChild != null && ballPosition.RightChild != null);

                ballPosition.BallCount += 1;
            }
        }

        /// <summary>
        /// Creates the ball gate model based on Level using Recursion and Dynamic tasks
        /// </summary>
        /// <param name="initial">Starting level of model</param>
        /// <param name="Level">Level of model</param>
        /// <param name="node">Node for which tree is created</param>
        /// <param name="tokenSource">TokenSource object for creating tasks</param>
        /// <param name="token">Token of the cancellation token source</param>
        public static void Traverse(int initial, int Level, NodeInfo node, CancellationTokenSource tokenSource, CancellationToken token)
        {
            try
            {
                if (initial >= Level)
                {
                    node.NodeId = ++_globalHeadId;
                    return;
                }

                node.LeftChild = new NodeInfo(NodeInfo.GetGateState());
                node.RightChild = new NodeInfo(NodeInfo.GetGateState());

                if (!token.IsCancellationRequested)
                {
                    ++initial;
                    var t1 = Task.Factory.StartNew(() => Traverse(initial, Level, node.LeftChild, tokenSource, token), token);
                    var t2 = Task.Factory.StartNew(() => Traverse(initial, Level, node.RightChild, tokenSource, token), token);

                    if (!t1.IsCanceled && !t2.IsCanceled)
                    {
                        Task.WaitAll(t1, t2);
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message +" Out of memory");
                tokenSource.Cancel();
                throw;
            }
            catch (AggregateException)
            {
                throw;
            }
        }

        #endregion
    }
}
