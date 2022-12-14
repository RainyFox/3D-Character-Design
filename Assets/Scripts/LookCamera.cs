using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    Transform camTrans;
    // Start is called before the first frame update
    void Start()
    {
        camTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = camTrans.position- transform.position;
        Quaternion r = Quaternion.LookRotation(v);
        transform.rotation = new Quaternion(r.x, r.y, 0, r.w);
    }
}
