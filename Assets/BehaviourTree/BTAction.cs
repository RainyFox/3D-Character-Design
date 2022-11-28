using UnityEngine;
using UnityEngine.Events;

namespace BehaviourTree
{
    public class BTAction : BTNode
    {
        protected float duration;
        protected float runTime;
        public event UnityAction OnEnter;
        public event UnityAction<BTAction> OnUpdate;
        public event UnityAction OnExit;

        public BTAction() : base(WaitType.Wait) {
            this.duration = 0;
            runTime = 0;
        }
        
        public BTAction(float duration) : base(WaitType.Wait) {
            this.duration = duration;
            runTime = 0;
        }

        public BTAction(float duration, WaitType waitType) : base(waitType) {
            this.duration = duration;
            runTime = 0;
        }

        bool isInit = false;
        public override void Execute() {
            if (status == Status.Finish) return;

            if (duration < 0) {
                status = Status.Running;
            }
            else {
                if (runTime > duration) {
                    Finish();
                }
                else {                    
                    status = Status.Running;
                    runTime += Time.deltaTime;
                }
            }            

            if (status == Status.Running) {
                if (!isInit) {
                    isInit = true;
                    OnEnter?.Invoke();
                }
                else OnUpdate?.Invoke(this);
            }
        }

        public void Finish() {
            status = Status.Finish;            
        }

        public override void Reset() {
            if(status == Status.Finish) OnExit?.Invoke();

            runTime = 0;
            status = Status.Idle;
            isInit = false;
        }
    }
}