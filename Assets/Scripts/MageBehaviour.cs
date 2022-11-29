using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class MageBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] float maxHp = 1000, seeRadius = 20, attackRadius = 12.5f, safeRadius = 5f;
    [SerializeField] LayerMask whoIsEnemy;
    TPCharacter character;
    Animator animator;
    public bool isFighting { get; private set; }
    public bool canAttack { get; private set; }
    public bool canRun { get; private set; }
    public float CurrHp { get; private set; }
    Transform attackTarget;
    Vector3 moveDir = Vector3.zero;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<TPCharacter>();
        animator = GetComponent<Animator>();
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

        character.Move(moveDir, false);
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
        moveDir = Vector3.zero;
        animator.SetBool("IsFighting", false);
    }

    public void WalkAction()
    {
        moveDir = Quaternion.Euler(0, Random.Range(0, 360), 0) * transform.forward * 0.5f;
        animator.SetBool("IsFighting", false);
    }
    public void OnCloseToActionEnter()
    {
        animator.SetBool("IsFighting", true);
    }
    public void OnCloseToActionUpdate(BTAction bTAction)
    {
        moveDir = Vector3.ProjectOnPlane(attackTarget.position - transform.position, Vector3.up).normalized;
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
