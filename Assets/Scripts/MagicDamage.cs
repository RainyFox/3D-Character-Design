using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    [SerializeField] float damage = 150;
    [SerializeField] LayerMask whoIsEnemy;
    private void OnCollisionEnter(Collision other)
    {
        int bitValue = 1 << other.gameObject.layer;
        if ((bitValue & whoIsEnemy.value) > 0)
        {
            other.gameObject.SendMessage("GetDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
