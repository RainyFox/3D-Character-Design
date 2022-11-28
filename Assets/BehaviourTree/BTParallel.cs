using System.Collections.Generic;

namespace BehaviourTree
{
    public class BTParallel : BTNode
    {
        private List<BTNode> children = new List<BTNode>();

        public BTParallel() : base(WaitType.NoWait) {}
        //public BTParallel(WaitType waitType) : base(waitType) { }

        public void AddChildNode(BTNode node) {
            node.parent = this;
            children.Add(node);
        }

        public override void Execute() {
            if (children == null || children.Count == 0) {
                throw new System.Exception("Sequence node can not be leaf.");
            }

            status = Status.Finish;
            foreach (BTNode child in children) {
                if (child.status != Status.Finish) {
                    status = Status.Running;
                    child.Execute();
                }
            }
            if (status == Status.Finish) Reset();
        }

        public override void Reset() {
            status = Status.Idle;
            foreach (BTNode child in children) child.Reset();
        }
    }
}