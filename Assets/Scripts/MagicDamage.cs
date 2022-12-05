using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    [SerializeField] float damage = 150;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("GetDamage", damage,SendMessageOptions.DontRequireReceiver);
        }
    }
}
