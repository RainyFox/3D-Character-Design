using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpCtrl : MonoBehaviour
{
    [SerializeField] float maxHp = 1000;
    [SerializeField] Image hpImage;
    Rigidbody rb;
    Collider rootCollider;
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Animator animatior;

    float currHp;

    // Start is called before the first frame update
    void Start()
    {
        animatior = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rootCollider = GetComponent<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        currHp = maxHp;

        SetRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currHp / maxHp, 0.1f);
    }

    void GetDamage(float damage)
    {
        currHp -= damage;
        if (currHp < 0)
        {
            currHp = 0;
            animatior.applyRootMotion = true;
            animatior.SetBool("IsDead", true);
        }
    }

    void SetRagdoll(bool active)
    {

    }

    void Dead()
    {
        SetRagdoll(true);
    }
}
