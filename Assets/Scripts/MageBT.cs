using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class MageBT : MonoBehaviour
{
    BTNode rootNode;
    MageBehaviour behaviour;
    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<MageBehaviour>();
        rootNode = TreeSetup();
    }

    // Update is called once per frame
    void Update()
    {

        if (rootNode != null) rootNode.Execute();
    }

    BTNode TreeSetup()
    {
        BTSelect root = new BTSelect();

        #region Children of root
        BTSelect aliveSelect = new BTSelect();
        BTAction deadAction = new BTAction();
        deadAction.priority = behaviour.GetDeadPriority;

        root.AddChildNode(aliveSelect);
        root.AddChildNode(deadAction);
        #endregion

        #region Children of alive
        BTSequence noFightSequence = new BTSequence();
        BTSelect fightSelect = new BTSelect();
        fightSelect.priority = () => behaviour.isFighting ? 100 : 0;
        aliveSelect.AddChildNode(noFightSequence);
        aliveSelect.AddChildNode(fightSelect);
        #endregion

        #region Children of noFight
        BTAction idleAction = new BTAction(Random.Range(1, 3));
        idleAction.OnEnter += behaviour.IdleAction;
        BTAction walkAction = new BTAction(Random.Range(3, 7));
        walkAction.OnEnter += behaviour.WalkAction;
        noFightSequence.AddChildNode(idleAction);
        noFightSequence.AddChildNode(walkAction);
        #endregion

        #region Children of fight
        BTAction closeToAction = new BTAction(-1, WaitType.NoWait);

        BTParallel attackParallel = new BTParallel();
        BTAction runBackAction = new BTAction(1.5f);

        attackParallel.priority = () => behaviour.canAttack ? 50 : 0;
        runBackAction.priority = () => behaviour.canRun ? 100 : 0;
        runBackAction.OnEnter += behaviour.RunbackAction;
        closeToAction.OnEnter += behaviour.OnCloseToActionEnter;
        closeToAction.OnUpdate += behaviour.OnCloseToActionUpdate;


        fightSelect.AddChildNode(closeToAction);
        fightSelect.AddChildNode(attackParallel);
        fightSelect.AddChildNode(runBackAction);
        #endregion

        #region Children of attack
        BTAction lookAtAction = new BTAction();
        BTAction attackAction = new BTAction();
        lookAtAction.OnUpdate += behaviour.LookatAction;
        attackAction.OnEnter += behaviour.OnAttackActionEnter;
        attackAction.OnUpdate += behaviour.OnAttackActionUpdate;
        attackParallel.AddChildNode(lookAtAction);
        attackParallel.AddChildNode(attackAction);
        #endregion

        return root;
    }
}