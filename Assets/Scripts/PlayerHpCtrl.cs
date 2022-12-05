using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpCtrl : MonoBehaviour
{
    [SerializeField] float maxHp = 1000;
    [SerializeField] Image hpImage;

    float currHp;

    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
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
            currHp = 0;
    }

}
