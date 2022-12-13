using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCtrl : MonoBehaviour
{
    [SerializeField] float maxHp = 1000;
    [SerializeField] Image hpImage;
    Rigidbody rootRigidbody;
    Collider rootCollider;
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Animator animatior;
    Rigidbody rb;
    public float CurrHp { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animatior = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rootRigidbody = GetComponent<Rigidbody>();
        rootCollider = GetComponent<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        CurrHp = maxHp;

        SetRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, CurrHp / maxHp, 0.1f);
        if (CurrHp <= 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void GetDamage(float damage)
    {
        CurrHp -= damage;
        if (CurrHp < 0)
        {
            CurrHp = 0;
            animatior.applyRootMotion = true;
            animatior.SetBool("IsDead", true);
        }
    }

    void SetRagdoll(bool active)
    {
        rootRigidbody.isKinematic = active;
        rootCollider.isTrigger = active;
        foreach (var rigidbody in rigidbodies)
        {
            if (rigidbody != rootRigidbody)
                rigidbody.isKinematic = !active;
        }
        foreach (var collider in colliders)
        {
            if (collider != rootCollider)
                collider.isTrigger = !active;
        }
    }
    //Called by dead animation
    void Dead()
    {
        animatior.enabled = false;
        SetRagdoll(true);
    }
}
