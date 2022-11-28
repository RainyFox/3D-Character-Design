using System.Collections.Generic;

namespace BehaviourTree
{
    public class BTSelect : BTNode
    {
        private List<BTNode> children = new List<BTNode>();

        public BTSelect() : base(WaitType.NoWait) {}
        //public BTSelect(WaitType waitType) : base(waitType) { }

        public void AddChildNode(BTNode node) {
            node.parent = this;
            children.Add(node);
        }

        BTNode runningNode = null;
        public override void Execute() {
            if (children == null || children.Count == 0) {
                throw new System.Exception("Selector node can not be leaf.");
            }
            
            if (runningNode == null || runningNode != null &&
                (runningNode.waitType == WaitType.NoWait || runningNode.status == Status.Finish)) {
                BTNode selectedNode = null;
                int selectedNodePriority = 0;
                foreach (BTNode child in children) {
                    if (selectedNode == null) {
                        selectedNode = child;
                        selectedNodePriority = child.priority != null ? child.priority() : 0;
                    }
                    else {
                        int checkPriority = child.priority != null ? child.priority() : 0;
                        if (checkPriority > selectedNodePriority) {
                            selectedNode = child;
                            selectedNodePriority = checkPriority;
                        }
                    }
                }

                if (runningNode != null) {
                    if(runningNode != selectedNode) runningNode.Reset();
                }
                runningNode = selectedNode;
            }

            if (runningNode != null) {
                if (runningNode.status == Status.Finish) {
                    Reset();
                    status = Status.Finish;
                }
                else {
                    status = Status.Running;
                    runningNode.Execute();
                }
            }            
        }

        public override void Reset() {
            status = Status.Idle;
            runningNode = null;
            foreach (BTNode child in children) child.Reset();
        }
    }
}
