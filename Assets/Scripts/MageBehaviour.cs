using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class MageBehaviour : MonoBehaviour
{
    [SerializeField] float maxHp = 1000, seeRadius = 20, attackRadius = 12.5f, safeRadius = 5f;
    [SerializeField] LayerMask whoIsEnemy;
    public bool isFighting { get; private set; }
    public bool canAttack { get; private set; }
    public bool canRun { get; private set; }
    public float CurrHp { get; private set; }
    Transform attackTarget;
    // Start is called before the first frame update
    void Start()
    {
        CurrHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        isFighting = false;
        canAttack = false;
        canRun = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, seeRadius, whoIsEnemy, QueryTriggerInteraction.Ignore);
        if (colliders.Length > 0)
        {
            isFighting = true;
            attackTarget = colliders[0].transform;
            float aiToEnemyDist = Vector3.Distance(transform.position, attackTarget.position);
            if (aiToEnemyDist < attackRadius) canAttack = true;
            if (aiToEnemyDist < safeRadius) canRun = true;
        }
    }

    public int GetDeadPriority()
    {
        return CurrHp <= 0 ? 100 : 0;
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 0.1f, 0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + offset, seeRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, attackRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + offset, safeRadius);
    }

    public void IdleAction()
    {

    }

    public void WalkAction()
    {

    }

    public void CloseToAction(BTAction bTAction)
    {

    }

    public void RunbackAction()
    {

    }

    public void LookatAction(BTAction bTAction)
    {

    }
    public void OnAttackActionEnter()
    {

    }
    public void OnAttackActionUpdate(BTAction bTAction)
    {

    }
}
