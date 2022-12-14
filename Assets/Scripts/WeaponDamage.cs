using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] float damage = 398;
    [SerializeField] LayerMask whoIsEnemy;

    Animator animator;
    private void Start()
    {
        animator = transform.root.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetFloat("AttackCurve") > 0)
        {
            int bitValue = 1 << other.gameObject.layer;
            if ((bitValue & whoIsEnemy.value) > 0)
            {
                other.gameObject.SendMessage("GetDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
