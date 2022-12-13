using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] float damage = 398;
    [SerializeField] LayerMask whoIsEnemy;
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
