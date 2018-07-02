using GatesBallGameApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BallGate
{
    [TestClass]
    public class NodeInfoUnitTest
    {
        [TestMethod]
        public void NodeInfo_Constructor_Test()
        {
            NodeInfo node = new NodeInfo(GateState.Left);

            Assert.IsNotNull(node);
            Assert.IsTrue(node.GateState == GateState.Left);
            Assert.IsNull(node.RightChild);
            Assert.IsNull(node.LeftChild);
            Assert.AreEqual(0, node.NodeId);
            Assert.AreEqual(0, node.BallCount);
        }

        [TestMethod]
        public void NodeInfo_GetGateState_Test()
        {
            GateState state = NodeInfo.GetGateState();

            Assert.IsTrue(state == GateState.Left || state == GateState.Right);
        }

        [TestMethod]
        public void NodeInfo_Dispose_Test()
        {
            NodeInfo node = new NodeInfo(GateState.Left);
            Assert.IsNotNull(node);
            node.Dispose();
        }
    }
}
