using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpCtrl : MonoBehaviour
{
    [SerializeField] float maxHp = 1000;
    [SerializeField] Image hpImage;
    Animator animatior;
    Rigidbody rb;
    float currHp;

    // Start is called before the first frame update
    void Start()
    {
        animatior = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currHp / maxHp, 0.1f);
        if (currHp <= 0)
        {
            rb.velocity = Vector3.zero;
        }
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

}
