using System;

namespace GatesBallGameApplication
{
    #region Enumerations

    /// <summary>
    /// Enumeration for maintaining the Gate state
    /// </summary>
    public enum GateState
    {
        /// <summary>
        /// Gate is left open
        /// </summary>
        Left = 0,

        /// <summary>
        /// Gate is right open
        /// </summary>
        Right
    }

    #endregion

    /// <summary>
    /// Model class for maintaining the node info in Ball 
    /// </summary>
    public class NodeInfo : IDisposable
    {
        #region Public properties

        /// <summary>
        /// Left child of the node
        /// </summary>
        public NodeInfo LeftChild { get; set; }

        /// <summary>
        /// Right child of the node
        /// </summary>
        public NodeInfo RightChild { get; set; }

        /// <summary>
        /// Gate state - whether Left or Right open
        /// </summary>
        public GateState GateState { get; set; }

        /// <summary>
        /// Number of balls reached to the node
        /// </summary>
        public int BallCount { get; set; }

        /// <summary>
        /// Id of the node
        /// </summary>
        public long NodeId { get; set; }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="state">Gate state - Left or Right</param>
        public NodeInfo(GateState state)
        {
            GateState = state;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the state of the Gate
        /// Generates a random number between 1 to 10000
        /// If the number generated is an even, returns Right as the gate state; else returns Left
        /// </summary>
        /// <returns></returns>
        public static GateState GetGateState()
        {
            Random random = new Random();
            int number = random.Next(1, 10000);

            return number % 2 == 0 ? GateState.Right : GateState.Left;
        }

        #endregion

        #region Implementing IDisposable methods

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                LeftChild = null;
                RightChild = null;
                GC.Collect();
                GC.SuppressFinalize(this);
                GC.WaitForPendingFinalizers();
            }
        }

        #endregion
    }
}
