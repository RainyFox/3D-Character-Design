using System.Collections.Generic;

namespace BehaviourTree
{
    public class BTSequence : BTNode
    {
        private List<BTNode> children = new List<BTNode>();
        
        public BTSequence() : base(WaitType.NoWait) {}
        //public BTSequence(WaitType waitType) : base(waitType) { }

        public void AddChildNode(BTNode node) {
            node.parent = this;
            children.Add(node);
        }

        BTNode runningNode = null;
        public override void Execute() {
            if (children == null || children.Count == 0) {
                throw new System.Exception("Sequence node can not be leaf.");
            }

            status = Status.Running;
            if (runningNode != null) {
                if (runningNode.status == Status.Running) {
                    runningNode.Execute();
                    return;
                }
            }
            runningNode = null;
            foreach (BTNode child in children) {
                if (child.status == Status.Idle) {
                    runningNode = child;
                    break;
                }
            }
            if (runningNode == null) {
                Reset();
                status = Status.Finish;
            }
            else runningNode.Execute();
        }

        public override void Reset() {
            status = Status.Idle;
            runningNode = null;
            foreach (BTNode child in children) child.Reset();
        }
    }
}