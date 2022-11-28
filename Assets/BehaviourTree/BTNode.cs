namespace BehaviourTree
{
    public enum Status
    {
        Idle,
        Finish,
        Running
    }

    public enum WaitType
    {
        NoWait,
        Wait
    }

    public delegate int PriorityDelegate();

    public abstract class BTNode
    {
        public BTNode parent;
        public Status status;
        public WaitType waitType;
        public PriorityDelegate priority;

        public BTNode(WaitType waitType) {
            parent = null;
            status = Status.Idle;
            this.waitType = waitType;
        }

        public abstract void Execute();
        public abstract void Reset();
    }
}